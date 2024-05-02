using System;
using System.IO;
using System.Net.Mail;

namespace dotnet_mailmessage_to_internet_message
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteEML_BodyText();
            WriteEML_BodyHtml();
        }


        static void WriteEML_BodyText()
        {
            string from = "james@mail.de";
            string to = "amelia@web.com";
            string subject = "I love you!";
            string body = "Do you want to marry me?";

            MailMessage mailMessage = new MailMessage(from, to, subject, body);

            string imf = mailMessage.ToInternetMessageFormat();

            DateTime ts = DateTime.Now;
            File.WriteAllText($"{ts.ToString("yyyy-MM-dd_HH-mm")}_TxtMail_TO_{to}.eml", imf);
        }


        static void WriteEML_BodyHtml()
        {
            string from = "james@mail.de";
            string to = "amelia@web.com";
            string subject = "I love you!";
            string heart = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAAkQAAAJEBVAVoGQAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAFISURBVDiNpZM/S0JRGMZ/71GIiAaXhghqymtj4WKDyM2PEBZ9gPZWRyen9r5DBS2FV1EvdT+AS44FURE5mUT+OW+LhpSa2bOcw/O+v+c8yxFV5T8KDy63rrtKqJMWzLKqBIutj5uVIHgHeEgk5psLc9simsDqvZieFy1cPwKIqlLfSR4iHAPzX9HKnQ2bNIDpWg9hbejhFsiR41VOpO4mUypaAmREw5f+uTRipoimwohmx8DjwIEEJGsUNicsTZayZYDXmQOgYQS82Qto0fS6Jgc0Z+DfCJEzG+Xys7GSAdp/gDsiHMSu/CcDsF6qXIqwC3SmgVUkEy1ULwDMwO0be780aYPuxwqV84FhhqeOVz0T1ThQGwHXRDXueP7psGm+b0WLfs1GGnGBPGABK5C3kUY8WvR/BMuk31h3kwkAp1QNxu1MDJhGny82eMnIn1weAAAAAElFTkSuQmCC";
            string img = $"<img src={heart} width=\"16\" height=\"16\" />";
            string body = $"Do you want to {img} <strong>marry</strong> {img} me? ";

            MailMessage mailMessage = new MailMessage(from, to, subject, body);
            mailMessage.IsBodyHtml = true;


            string imf = mailMessage.ToInternetMessageFormat();

            DateTime ts = DateTime.Now;
            File.WriteAllText($"{ts.ToString("yyyy-MM-dd_HH-mm")}_HtmlMail_TO_{to}.eml", imf);
        }
    }
}
