using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Text;

public static class MailMessageExtensions
{



    /// <summary>
    /// Internet Message Format (IMF) is the standardized ASCII-based syntax required by SMTP for all 
    /// email message bitstreams used by a message transfer agent, sometimes referred to as a mail transfer agent or MTA, 
    /// when moving messages between computers. IMF is standardized by RFC 5322.
    /// 
    /// https://www.loc.gov/preservation/digital/formats/fdd/fdd000393.shtml
    /// 
    /// IMF can be save to an EML file:
    /// EML, short for electronic mail or email, is a file extension for an email message saved to a file in 
    /// the Internet Message Format protocol for electronic mail messages. 
    /// It is the standard format used by Microsoft Outlook Express as well as some other email programs.    
    /// Since EML files are created to comply with industry standard RFC 5322, 
    /// EML files can be used with most email clients, servers and applications. 
    /// 
    /// Die EML-Dateierweiterung wird verwendet, um E-Mail-Nachrichten im RFC-822-Format zu speichern. 
    /// Viele Anwendungen wie Microsoft Outlook, Thunderbird, Apple Mail, Windows Mail und 
    /// andere verwenden das EML-Format zum Exportieren und Importieren von E-Mails. 
    /// </summary>
    public static string ToInternetMessageFormat(this MailMessage message)
    {
        var assembly = typeof(SmtpClient).Assembly;
        var mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

        using (var memoryStream = new MemoryStream())
        {
            // Get reflection info for MailWriter contructor
            var mailWriterContructor = mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);

            // Construct MailWriter object with our FileStream
            var mailWriter = mailWriterContructor.Invoke(new object[] { memoryStream });

            // Get reflection info for Send() method on MailMessage
            var sendMethod = typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

            // Call method passing in MailWriter
            sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true, true }, null);

            // Finally get reflection info for Close() method on our MailWriter
            var closeMethod = mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);

            // Call close method
            closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);

            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }
    }

}
