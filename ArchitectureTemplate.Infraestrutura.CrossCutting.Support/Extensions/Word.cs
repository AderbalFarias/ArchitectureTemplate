using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions
{
    public class Word
    {

        public void ReplaceAll(string fileName, IDictionary<string, string> entityDictionary, IList<IDictionary<string, string>> entityAditionalDictionary = null, string tagForeachReplace = null, string dataReplace = null)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(fileName, true))
            {
                string documentText;
                string foreachReplace = string.Empty;

                using (StreamReader reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    documentText = reader.ReadToEnd();
                }

                if (entityAditionalDictionary != null && entityAditionalDictionary.Any() 
                    && !string.IsNullOrEmpty(tagForeachReplace) && !string.IsNullOrEmpty(dataReplace))
                {
                    foreachReplace = entityAditionalDictionary.Aggregate(foreachReplace, (current1, entity) 
                        => current1 + entity.Aggregate(dataReplace, (current, item) 
                        => current.Replace($"[{item.Key}]", item.Value)));

                    documentText = documentText.Replace(tagForeachReplace, foreachReplace);
                }

                documentText = entityDictionary.Aggregate(documentText, (current, item) 
                    => current.Replace($"[{item.Key}]", item.Value));

                using (StreamWriter writer = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    writer.Write(documentText);
                }
            }
        }

        //Microsoft.Office.Interop.Word não é ideal para ambiente web (não funciona corretamente)
        //public void ReplaceAll(object fileName, IDictionary<string, string> entityDictionary)
        //{
        //    Application word = null;
        //    Document doc = null;

        //    word = new Application();
        //    object missing = Type.Missing;

        //    doc = word.Documents.Open(ref fileName,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing);

        //    doc.Activate();

        //    Find fnd = word.ActiveWindow.Selection.Find;

        //    fnd.ClearFormatting();
        //    fnd.Replacement.ClearFormatting();
        //    fnd.Forward = true;
        //    fnd.Wrap = WdFindWrap.wdFindContinue;

        //    foreach (var item in entityDictionary)
        //    {
        //        fnd.Text = $"<{item.Key}>";
        //        fnd.Replacement.Text = $"{item.Value}";
        //        fnd.Execute(Replace: WdReplace.wdReplaceAll);
        //    }

        //    doc.Save();
        //    doc.Close(ref missing, ref missing, ref missing);
        //    word.Application.Quit(ref missing, ref missing, ref missing);
        //}

        //public void ReplaceRange(object fileName, IDictionary<string, string> entityDictionary)
        //{
        //    Application word = new Application();
        //    object missing = Type.Missing;

        //    var doc = word.Documents.Open(ref fileName,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing);

        //    doc.Activate();

        //    //region substituir por paragrafo
        //    foreach (Range tmpRange in doc.StoryRanges)
        //    {
        //        foreach (var item in entityDictionary)
        //        {
        //            tmpRange.Find.Text = $"<{item.Key}>";
        //            tmpRange.Find.Replacement.Text = item.Value;
        //            //tmpRange.Find.Replacement.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

        //            tmpRange.Find.Wrap = WdFindWrap.wdFindContinue;
        //            object replaceAll = WdReplace.wdReplaceAll;

        //            tmpRange.Find.Execute(ref missing, ref missing, ref missing,
        //                ref missing, ref missing, ref missing, ref missing,
        //                ref missing, ref missing, ref missing, ref replaceAll,
        //                ref missing, ref missing, ref missing, ref missing);
        //        }
        //    }

        //    doc.Save();
        //    doc.Close(ref missing, ref missing, ref missing);
        //    word.Application.Quit(ref missing, ref missing, ref missing);
        //}
    }
}