using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace SendEmail
{
    class Program
    {
        static void Main(string[] args)
        {

            TextReader iniFile = null;
            string strLine = null;
            string[] keyPair = null;

            string email_text_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\email_text.txt";
            
            // Test if input arguments were supplied:
            if (args.Length != 0)
            {
                email_text_path = args[0];
                //System.Console.WriteLine(email_text_path);
            }

            string sender = "";
            string recipient = "";
            string to_copy = "";
            string email_subject = "Send Email Message";
            string smtpServer = "";
            int serverPort = 587;
            string login = "";
            string password = "";

            string CredentialsPathFile = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SendEmail.ini";
            if (File.Exists(CredentialsPathFile))
            {
                try
                {
                    iniFile = new StreamReader(CredentialsPathFile);

                    strLine = iniFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim();

                        if (strLine != "")
                        {
                            keyPair = strLine.Split(new char[] { '=' }, 2);
 
                            if (keyPair.Length > 1)
                            {
                                if (keyPair[0].ToUpper() == "EMAIL_TEXT_PATH")
                                    email_text_path = keyPair[1].Trim();
                                if (keyPair[0].ToUpper() == "SENDER")
                                    sender = keyPair[1].Trim();
                                if (keyPair[0].ToUpper() == "RECIPIENT")
                                    recipient = keyPair[1].Trim();
                                if (keyPair[0].ToUpper() == "COPY")
                                    to_copy = keyPair[1].Trim();
                                if (keyPair[0].ToUpper() == "EMAIL_SUBJECT")
                                    email_subject = keyPair[1].Trim();
                                if (keyPair[0].ToUpper() == "SMTPSERVER")
                                    smtpServer = keyPair[1].Trim();
                                if (keyPair[0].ToUpper() == "SERVERPORT")
                                    serverPort = int.Parse(keyPair[1].Trim());
                                if (keyPair[0].ToUpper() == "LOGIN")
                                    login = keyPair[1].Trim();
                                if (keyPair[0].ToUpper() == "PASSWORD")
                                    password = keyPair[1].Trim();

                            }

                        }

                        strLine = iniFile.ReadLine();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();
                }
            }
            else
                throw new FileNotFoundException("Unable to locate " + CredentialsPathFile);

            if (File.Exists(email_text_path))
            {
                // This path is a file
                ProcessFile(email_text_path);
            }
            else if (Directory.Exists(email_text_path))
            {
                // This path is a directory
                ProcessDirectory(email_text_path);
            }
 
            // Process all files in the directory passed in, recurse on any directories 
            // that are found, and process the files they contain.
            void ProcessDirectory(string targetDirectory)
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                    ProcessFile(fileName);

                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory);
            }

            // Insert logic for processing found files here.
            void ProcessFile(string path)
            {
                Console.WriteLine("Processed file '{0}'.", path);
                if (File.Exists(path))
                {
                    string[] text = System.IO.File.ReadAllLines(path, Encoding.GetEncoding("Windows-1251"));
                    var CountArray = text.Length;
                    var Subject = email_subject;
                    //var Body = text[CountArray - 1];
                    var Body = String.Join("\n", text);
                    MailAddress From = new MailAddress(sender);
                    MailAddress To   = new MailAddress(recipient);
                    var msg = new MailMessage(From, To)
                    {
                        Body = Body,
                        Subject = Subject
                    };
                    if (to_copy != "") {
                        String[] elements = to_copy.Split(';');
                        foreach (var element in elements)
                        {
                            msg.CC.Add(new MailAddress(element.Trim()));
                        }
                    }
                    var smtpClient = new SmtpClient(smtpServer, serverPort)
                    {
                        Credentials = new NetworkCredential(login, password),
                        EnableSsl = true
                    };
                    smtpClient.Send(msg);
                    File.Delete(path);
                }
                else
                    throw new FileNotFoundException("Unable to locate " + path);
            }
        }
    }
 
}
