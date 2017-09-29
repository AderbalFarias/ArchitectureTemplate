﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class MensagensResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MensagensResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Resources.MensagensResou" +
                            "rce", typeof(MensagensResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to E-mail já cadastrado no sistema.
        /// </summary>
        public static string EmailExistente {
            get {
                return ResourceManager.GetString("EmailExistente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocorreu um problema ao tentar realizar atualização de dados.
        /// </summary>
        public static string ErroAtualizar {
            get {
                return ResourceManager.GetString("ErroAtualizar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocorreu um problema ao tentar cadastrar dados.
        /// </summary>
        public static string ErroCadastrar {
            get {
                return ResourceManager.GetString("ErroCadastrar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocorreu um problema ao tentar carregar dados para a tela.
        /// </summary>
        public static string ErroCarregar {
            get {
                return ResourceManager.GetString("ErroCarregar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao realizar download de arquivo.
        /// </summary>
        public static string ErroDownload {
            get {
                return ResourceManager.GetString("ErroDownload", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocorreu um problema ao tentar excluir dados.
        /// </summary>
        public static string ErroExcluir {
            get {
                return ResourceManager.GetString("ErroExcluir", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocorreu um erro ao tentar salvar dados.
        /// </summary>
        public static string ErroSalvar {
            get {
                return ResourceManager.GetString("ErroSalvar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não há empreendimento associado para o processo.
        /// </summary>
        public static string ProcessoSemEmpreendimento {
            get {
                return ResourceManager.GetString("ProcessoSemEmpreendimento", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dados atualizados com sucesso.
        /// </summary>
        public static string SucessoAtualizar {
            get {
                return ResourceManager.GetString("SucessoAtualizar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dados cadastrados com sucesso.
        /// </summary>
        public static string SucessoCadastrar {
            get {
                return ResourceManager.GetString("SucessoCadastrar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dados cadastrados com sucesso, porém ocorreu um erro ao enviar email.
        /// </summary>
        public static string SucessoCadastrarErroEmail {
            get {
                return ResourceManager.GetString("SucessoCadastrarErroEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dados cadastrados com sucesso e email enviado.
        /// </summary>
        public static string SucessoCadastrarSucessoEmail {
            get {
                return ResourceManager.GetString("SucessoCadastrarSucessoEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Atividade concluída com sucesso.
        /// </summary>
        public static string SucessoWorkflow {
            get {
                return ResourceManager.GetString("SucessoWorkflow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Processo cadastrado, porém ocorreu um erro ao tentar salvar dados de inicialização para o fluxo do processo.
        /// </summary>
        public static string WarningSalvarProcesso {
            get {
                return ResourceManager.GetString("WarningSalvarProcesso", resourceCulture);
            }
        }
    }
}