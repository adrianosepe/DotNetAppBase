using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Grapp.AppBase.Std.Exceptions.Internals;

namespace Grapp.AppBase.Std.Exceptions.Manager
{
    [Localizable(isLocalizable: false)]
    public class ExceptionManager
    {
        public static readonly ExceptionManager Instance;

        private IExceptionConfig _config;

        static ExceptionManager() => Instance = new ExceptionManager();

        internal ExceptionManager() => DefaultErrorMessage = "Ocorreu um erro inesperado em algum processo interno. Tente novamente.";

        public bool AllowSupportEmail { get; set; }

        public bool AttachmentsPrintScreen { get; set; }

        public bool AutoSendEmail { get; set; }

        public string DefaultErrorMessage { get; set; }

        public string EmailFrom { get; set; }

        public string EmailSmtpServer { get; set; }

        public string EmailTo { get; set; }

        public string SupportEmail { get; set; }

        public string EmailSmtpUser { get; set; }

        public string EmailSmtpPwd { get; set; }

        public bool EmailSmtpAuth { get; set; }

        public int EmailSmtpPort { get; set; }

        public event FormatEmailExceptionEventHandler FormatEmailException;

        public static string DefaultFormatException(Exception exception)
        {
            var msg = Encoding.UTF8.GetString(bytes: XHelper.Resources.LoadResource(typeof(ExceptionManager).Assembly,
                resourceName: "Manager.Resources.EmailErrorContent.html"));

            try
            {
                msg = msg.Replace(oldValue: "{NAME}", AppDomain.CurrentDomain.FriendlyName);
                msg = msg.Replace(oldValue: "{VERSION}", newValue: Assembly.GetEntryAssembly().GetName().Version.ToString());
                msg = msg.Replace(oldValue: "{MACHINE}", Environment.MachineName);
                msg = msg.Replace(oldValue: "{STACKTRACE}", newValue: FormatString(Environment.StackTrace, formatNewLine: true));
                msg = msg.Replace(oldValue: "{EXMESSAGE}", newValue: FormatString(exception.Message, formatNewLine: true));
                msg = msg.Replace(oldValue: "{EXCEPTIONS}",
                    newValue: FormatString(message: GetExceptions(exception, level: 0), formatNewLine: false));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(message: ex.ToString());
            }

            return msg;
        }

        private static string FormatString(string message, bool formatNewLine)
        {
            if (!formatNewLine) return message ?? string.Empty;

            return (message ?? string.Empty).Replace(Environment.NewLine, newValue: "<br />");
        }

        private static string GetExceptions(Exception exception, int level)
        {
            var msg = Encoding.UTF8.GetString(bytes: XHelper.Resources.LoadResource(typeof(ExceptionManager).Assembly,
                resourceName: "Manager.Resources.ExceptionContent.html"));

            msg = msg.Replace(oldValue: "{INNERLEVEL_PX}", newValue: GetInnerLevel(level));
            msg = msg.Replace(oldValue: "{EXMESSAGE}", exception.Message);
            msg = msg.Replace(oldValue: "{EXLEVEL}", newValue: level.ToString(CultureInfo.InvariantCulture));
            msg = msg.Replace(oldValue: "{EXTYPE}", exception.GetType().FullName);
            msg = msg.Replace(oldValue: "{EXMESSAGE}", newValue: FormatString(exception.Message, formatNewLine: true));
            msg = msg.Replace(oldValue: "{EXSTACKTRACE}", newValue: FormatString(exception.StackTrace, formatNewLine: true));
            msg = msg.Replace(oldValue: "{EXSOURCE}", newValue: FormatString(exception.Source, formatNewLine: true));

            if (exception.InnerException != null) msg += GetExceptions(exception.InnerException, level: level + 1);

            return msg;
        }

        private static string GetInnerLevel(int level)
        {
            if (level <= 0) return 0.ToString(CultureInfo.InvariantCulture);

            return (15 * level).ToString(CultureInfo.InvariantCulture);
        }

        public MailMessage CreateAutoMailMessage(string title, string body)
        {
            var message = new MailMessage(EmailFrom, EmailTo, title, body)
                {
                    IsBodyHtml = true
                };

            if (AttachmentsPrintScreen && IsConfigRegistered())
            {
                var fileName = Path.ChangeExtension(path: Path.GetTempFileName(), extension: "png");
                _config.CaptureScreen(fileName);

                message.Attachments.Add(item: new Attachment(fileName));
            }

            return message;
        }

        public MailMessage CreateSupportMessage(string title, string body)
        {
            var message = new MailMessage(EmailFrom, to: SupportEmail ?? EmailTo, title, body)
                {
                    IsBodyHtml = true
                };

            if (AttachmentsPrintScreen && IsConfigRegistered())
            {
                var fileName = Path.ChangeExtension(path: Path.GetTempFileName(), extension: "png");
                _config.CaptureScreen(fileName);

                message.Attachments.Add(item: new Attachment(fileName));
            }

            return message;
        }

        public string FormatException(Exception exception, bool useDefaultLayout = false)
        {
            if (FormatEmailException != null && !useDefaultLayout)
            {
                var e = new FormatEmailExceptionEventArgs(exception);
                FormatEmailException(sender: this, e);

                return e.Output.ToString();
            }

            return DefaultFormatException(exception);
        }

        public void HandleException(Exception exception)
        {
            var newGuid = Guid.NewGuid();
            var title = $"Protocol ID: {newGuid}";
            var body = FormatException(exception);

            if (AutoSendEmail) SendEmail(message: CreateAutoMailMessage(title, body));

            if (IsConfigRegistered()) _config.Execute(exception, newGuid, message: CreateSupportMessage(title, body));
        }

        public void RegisterExceptionConfig(IExceptionConfig config)
        {
            _config?.UnregisterManager(exceptionManager: this);

            _config = config;
            _config?.RegisterManager(exceptionManager: this);
        }

        public bool SendEmail(MailMessage message)
        {
            try
            {
                var mailClient = new SmtpClient(EmailSmtpServer, EmailSmtpPort);

                if (EmailSmtpAuth)
                {
                    mailClient.EnableSsl = true;
                    mailClient.Timeout = 10000;
                    mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mailClient.UseDefaultCredentials = false;
                    mailClient.Credentials = new NetworkCredential(EmailSmtpUser, EmailSmtpPwd);
                }

                mailClient.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsConfigRegistered() => _config != null;
    }
}