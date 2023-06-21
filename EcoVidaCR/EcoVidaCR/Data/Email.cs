using EcoVidaCR.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace EcoVidaCR.Data
{
    public class Email
    {
        public void Enviar(string correo, Voluntariados voluntario)
        {
            try
            {
                var doc = new iTextSharp.text.Document();

                MemoryStream memory = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(doc, memory);


                doc.Open();
                var imagen = iTextSharp.text.Image.GetInstance("wwwroot/img/pdf/imgEco.png");
                imagen.Alignment = Element.ALIGN_CENTER;
                imagen.ScaleToFit(300f, 300f);
                Paragraph title = new Paragraph();
                title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f, BaseColor.BLACK);
                title.Add("ECOVIDA COSTA RICA");
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph(""));
                doc.Add(new Paragraph(""));
                doc.Add(new Paragraph("\r\n"));
                Paragraph texto = new Paragraph();
                texto.Alignment = Element.ALIGN_JUSTIFIED;
                //doc.Add(new Paragraph("EcoVidaCR"));
                texto.Add("Nos complace enormemente que hayas utilizado la página EcoVidaCR y " +
                    "que te hayas interesado en participar en un voluntariado para ayudar al medio ambiente! " +
                    "Queremos expresarte nuestro sincero agradecimiento por tu compromiso y disposición para " +
                    "hacer una diferencia positiva en nuestro entorno natural." +
                    "En un mundo donde la preservación del medio ambiente se ha vuelto más crucial que nunca, " +
                    "tu interés y participación en actividades voluntarias son invaluables. Al unirte a nuestro " +
                    "equipo de voluntarios, demuestras una preocupación genuina por el planeta y un deseo de marcar " +
                    "una diferencia tangible en la conservación de nuestros recursos naturales.\r\n\r\nYa sea que te " +
                    "hayas unido a nosotros para plantar árboles, limpiar playas, educar a la comunidad o participar " +
                    "en cualquier otra actividad relacionada con la protección del medio ambiente, tu contribución es vital. " +
                    "Cada acción que emprendemos juntos tiene un impacto significativo en la salud y el equilibrio de nuestro " +
                    "ecosistema.\r\n\r\nNos gustaría destacar que tu dedicación y tiempo dedicados a este esfuerzo son " +
                    "inmensamente apreciados. Tu participación en un voluntariado no solo contribuye a la preservación " +
                    "del medio ambiente, sino que también inspira a otros a seguir tu ejemplo. Juntos, estamos creando " +
                    "conciencia y promoviendo un cambio positivo en nuestras comunidades.\r\n\r\nUna vez más, queremos " +
                    "expresar nuestro más profundo agradecimiento por tu compromiso con EcoVidaCR y por tu deseo de participar " +
                    "en nuestros proyectos de voluntariado. Esperamos trabajar contigo y lograr grandes cosas en beneficio del " +
                    "medio ambiente.");
                doc.Add(texto);
                doc.Add(new Paragraph("\r\n"));
                doc.Add(new Paragraph("Los datos generales del voluntariado son los siguientes:"));
                doc.Add(new Paragraph("Nombre:   "+voluntario.nombreVoluntariado));
                doc.Add(new Paragraph("Descripcion:   "+voluntario.descripcion));
                doc.Add(new Paragraph("\r\n"));
                doc.Add(new Paragraph("A continuación se presentan los datos de contacto del voluntariado que elegiste"));
                doc.Add(new Paragraph("Correo:   "+voluntario.correo));
                doc.Add(new Paragraph("Telefono:   "+voluntario.telefono));
                doc.Add(new Paragraph("\r\n\r\n"));
                doc.Add(new Paragraph("¡Gracias por ser parte de nuestro equipo!"));
                doc.Add(new Paragraph("Con gratitud y aprecio, el equipo de EcoVidaCR"));
                doc.Add(new Paragraph("\r\n"));

                doc.Add(imagen);

                writer.CloseStream = false;
                doc.Close();
                memory.Position = 0;






                //se crea la instancia del email
                MailMessage email = new MailMessage();

                //se agregan los destinatarios
                email.To.Add(new MailAddress(correo));

                //se agrega el emisor
                email.From = new MailAddress("distribucionespacificocr2022@gmail.com");

                //se agrega el asunto del email
                email.Subject = "Datos sobre el voluntariado en plataforma web EcoVidaCR";

                //se contruye el contenido del email
                string html = "Reciba un coordial saludo de parte del equipo EcoVidaCr, a continuación te adjuntamos" +
                    " más informacion sobre el voluntariado de tu interés, gracias por formar parte del cambio";
                


                //se incia que el contenido es en html
                email.IsBodyHtml = true;

                //se indica la prioridad del email
                email.Priority = MailPriority.Normal;

                email.Attachments.Add(new Attachment(memory, "Voluntariado.pdf"));
                //se crea la instancia de la vista  en html para el cuerpo del email
                AlternateView view = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8,
                    MediaTypeNames.Text.Html);

                //se agrega la vista html al email
                email.AlternateViews.Add(view);

                //Configuracion del protocolo de comunicación smtp
                //protocolo de transferencia simple de correo
                SmtpClient smtp = new SmtpClient();

                //ojo se indica el nombre del servidor de correo donde tenemos el buzon
                smtp.Host = "smtp.gmail.com";

                //es muy importante conocer el puerto de comunicación del servidor email
                smtp.Port = 587;

                //bandera para indicar que utiliza seguridad a nivel de protocolo
                smtp.EnableSsl = true;

                //bandera para indicar que no utiliza las credenciales por default
                smtp.UseDefaultCredentials = false;

                //se indica las credencias ojo  muy importante habilitar dentro de la seguridad del buzon
                //la opción de acceso aplicaciones poco segura esto lo presenta el servidor de gmail
                smtp.Credentials = new NetworkCredential("ecovidacrpuravida@gmail.com", "dnljuutqqjyqvidv");

                //bien, enviamos en correo
                smtp.Send(email);

                //siempre tenemos que liberar los recursos de los object utilizados
                smtp.Dispose();
                email.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//Cierre enviar
    }
}
