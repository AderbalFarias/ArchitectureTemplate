using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using ArchitectureTemplate.Mvc.Models.Shared;

namespace ArchitectureTemplate.Mvc.Controllers.Shared
{
    [Authorize]
    public abstract class CustomController : Controller
    {
        #region Properties

        public Message MensagemTemporaria
        {
            get { return (Message)TempData["ExibirMessagem"]; }
            set { TempData["ExibirMessagem"] = value; }
        }

        public AppUser CurrentUser => new AppUser(User as ClaimsPrincipal);

        #endregion

        #region Constructors
        #endregion

        #region Exibir mensagem temporária

        public void ExibirMensagemTemporaria()
        {
            if (MensagemTemporaria != null)
            {
                ViewBag.ExibirMessagem = MensagemTemporaria;
            }
        }

        /// <summary>
        /// Exibe um mensagem na tela
        /// </summary>
        /// <param name="mensagem">mensagem desejada</param>
        /// <param name="tipoMensagem">tipos: Sucesso, Informação, Warning, Error</param>
        /// <param name="timeout">tempo para exibição</param>
        public void ShowMessageDialog(string mensagem, Message.MessageKind tipoMensagem, int? timeout = null)
        {
            var msg = new Message
            {
                Kind = tipoMensagem,
                Title = mensagem,
                TimeoutSeconds = timeout,
                Detail = ""
            };

            MensagemTemporaria = msg;
        }

        /// <summary>
        /// Em caso de erro utilizar essa sobrecarga que também gera o log da exception
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="exception">somente em caso de erro</param>
        /// <param name="timeout">tempo para exibição</param>
        public void ShowMessageDialog(string mensagem, Exception exception, int? timeout = null)
        {
            //const string br = "<br/>";

            //var etype = exception.GetType().ToString();
            //var message = exception.Message ?? "null";
            //var trace = exception.StackTrace ?? "null";
            //var inner = exception.InnerException != null ? exception.InnerException.ToString() : "null";

            //trace = trace.Replace(Environment.NewLine, br);

            var msg = new Message
            {
                Kind = Message.MessageKind.Error,
                Title = mensagem,
                TimeoutSeconds = timeout,
                Detail = "Erro"
                //<b>- ExceptionType</b>: " + etype + br +
                  //       "<b>- Message</b>: " + message + br +
                    //     "<b>- Stack trace</b>: " + trace + br +
                      //   "<b>- Inner</b>: " + inner
            };

            MensagemTemporaria = msg;

            LogException(exception);
        }

        #endregion

        #region Registrar exception e gerar log de erro
       
        /// <summary>
        /// pega as exceptions geradas na controller
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            LogException(filterContext.Exception);
        }
        
        /// <summary>
        /// Gerar log do erro
        /// </summary>
        /// <param name="exception"></param>
        protected void LogException(Exception exception)
        {
            try
            {
                var rd = HttpContext.Request.RequestContext.RouteData;
                var controller = rd.GetRequiredString("controller");
                var action = rd.GetRequiredString("action");

                var local = Server.MapPath(@"~/Files/Exceptions");

                if (!Directory.Exists(local)) 
                    Directory.CreateDirectory(local);

                var dt = DateTime.Now;

                var fileName = $"{dt.ToString("yyyyMMdd-HHmmss")}-{exception.GetType().Name}-{controller}-{action}.txt";

                fileName = Path.Combine(local, fileName);

                try
                {
                    using (var sw = new StreamWriter(fileName, true, Encoding.GetEncoding("ISO-8859-1")))
                    {
                        sw.Write(exception.ToString(), 0, exception.ToString().Length);
                    }
                }
                catch (Exception)
                {
                    var bf = new BinaryFormatter();
                    using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        bf.Serialize(fs, exception);
                    }
                }
                
            }
            catch
            { }
        }
        #endregion

        #region Download de Arquivos

        #endregion
    }
}