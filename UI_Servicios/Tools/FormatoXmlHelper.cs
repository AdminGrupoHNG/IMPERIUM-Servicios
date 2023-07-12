using BL_Servicios;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using BE_Servicios;
using System.Reflection;

namespace UI_Servicios.Tools
{
    public class FormatoXmlHelper
    {
        private readonly blFormatoDocumentos unit;

        private string template;
        private string param1, param2, param3;
        private string image;
        private string textoXML = "", sApertura = "", sCierre = "", sApertura2 = "", sCierre2 = "", sApertura3 = "", sCierre3 = "", XMLIMAGEN = "", txtXMLImagen = "";

        public FormatoXmlHelper()
        {
            unit = new blFormatoDocumentos();
            template = GetTemplate();
            param1 = GetParamValues("PERSONAL OPERATIVO Y JORNADA LABORAL", "@tablaCotizacion");
            param2 = GetParamValues("DIAGNOSTICO", "@tablaSedes");
            param3 = GetParamValues("ACTIVIDADES A REALIZAR", "@tablaAlcance");
            image = GetParamImagesOld();
        }

        private string GetTemplate()
        {
            var plantilla = unit.ConsultaVariosFormato<eFormatoMD_General>(opcion: 1, cod_formato: "DM023", cod_solucion: "004");
            if (plantilla == null) return null;
            return plantilla[0].dsc_wordMLText;
        }

        private string GetParamValues(string tituloCercano, string nombreTabla)
        {
            try
            {
                //template
                int nPosA = -1; int nPosB = -1;
                if (nPosA == -1) { nPosA = template.IndexOf(tituloCercano); sApertura = "<w:tbl>"; }
                if (nPosB == -1) { nPosB = template.IndexOf(nombreTabla); sCierre = "</w:tr></w:tbl>"; }
                string textP1 = template.Substring(nPosA, (nPosB - nPosA) + sCierre.Length + 45);

                if (nPosA > 1) { nPosA = textP1.IndexOf("<w:tbl><w:tblPr>"); sApertura = "<w:tbl><w:tblPr>"; }
                if (nPosB > 1) { nPosB = textP1.IndexOf("</w:tr></w:tbl>"); sCierre = "</w:tr></w:tbl>"; }
                textoXML = textP1.Substring(nPosA, (nPosB - nPosA) + sCierre.Length);
                return textoXML;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Obtener Parámetros");
                return null;
            }
        }

        public void ShowReport<T>(List<T> query, string[] cabeceraPersonal, string[] cabeceraSedes, List<string[]> filasPersonal, List<string[]> filasSedes, List<string> filasAlcance = null) where T : class, new()
        {
            string rutaImagenPng = ToolHelper.downloadsFolderPath + ToolHelper.imagePngFile;

            RichEditControl frm = new RichEditControl();

            var image2 = Image.FromFile(rutaImagenPng);

            var document = new RichEditControl();
            document.Document.Images.Append(image2);

            string imageXml = document.WordMLText;

            var tablePersonal = GetCustomtable(cabeceraPersonal, filasPersonal, 1); // '1'-> tabla personal
            var tableSedes = GetCustomtable(cabeceraSedes, filasSedes, 2); // '2' -> tabla sedes
            var rowsAlcance = GetCustomRowList(filasAlcance);
            var newXmlOne = template.Replace(param1, tablePersonal);
            newXmlOne = newXmlOne.Replace(param2, tableSedes);
            newXmlOne = newXmlOne.Replace(param3, rowsAlcance);
            newXmlOne = newXmlOne.Replace(image, GetParamImagesNew(imageXml));

            frm.WordMLText = GetTemplatesWithValues<T>(query, newXmlOne);

            ExportarWordDocument(frm);
        }

        private String GetTemplatesWithValues<T>(List<T> query, string formato) where T : class, new()
        {
            var entity = query.First();
            var paramList = GetParams();

            paramList.Where((fil) => fil.flg_asignado.EndsWith("SI"))
                .ToList().ForEach((p) =>
                {
                    var properties = entity.GetType().GetTypeInfo().GetProperties();
                    foreach (var k in properties)
                    {
                        if (k.Name.ToString().ToLower().Trim().Equals(p.dsc_columna_asociada.ToLower().Trim()))
                        {
                            var value = k.GetValue(entity);
                            if (value != null)
                            {
                                var param = p.dsc_formatoMD_parametro.ToString();
                                var newString = formato.Replace(param, value.ToString());
                                formato = newString;
                            }
                        }
                    }
                });

            paramList.ForEach((pr) =>
            {
                var param = pr.dsc_formatoMD_parametro.ToString();
                var newString = formato.Replace(param, "");
                formato = newString;
            });

            return formato;
        }

        private List<eFormatoMD_Parametro> GetParams()
        {
            var paramList = unit.ConsultaVariosFormato<eFormatoMD_Parametro>(opcion: 6, cod_formato: "DM023", cod_solucion: "002");

            string tmp = template.ToLower();
            paramList.ForEach(j => j.flg_asignado = "NO");

            paramList.ToList()
                 .ForEach((obj) =>
                 {
                     string pms = obj.dsc_formatoMD_parametro.ToLower();
                     if (tmp.Contains(pms))
                     {
                         var index = paramList.IndexOf(obj);
                         paramList[index].flg_asignado = "SI";
                     }
                 });
            return paramList.ToList();
        }

        private void ExportarWordDocument(RichEditControl editControl)
        {
            string downloadsFolderPath = ToolHelper.downloadsFolderPath + ToolHelper.nameWordFile;

            try
            {
                editControl.SaveDocument(downloadsFolderPath, DocumentFormat.OpenXml);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }

            Process.Start(downloadsFolderPath);
        }

        private string GetCustomtable(string[] HeaderValues, List<string[]> RowsValues, int tipoTabla)
        {
            string xml = $"{GenerateHeader(HeaderValues, tipoTabla)} ";
            for (int i = 0; i < RowsValues.Count(); i++)
            {
                xml += $"{GenerateRows(RowsValues[i], i + 1)}";
            }
            return string.Concat(xml, CloseTable);
        }

        private string GetCustomRowList(List<string> RowsValue)
        {
            string xml = "";
            for (int i = 0; i < RowsValue.Count(); i++)
            {
                xml += $"{RowListContent(RowsValue[i])}";
            }
            return xml;
        }

        private string GenerateHeader(string[] content, int tipoTabla)
        {
            string xml = $"{HeaderStyle()} {OpenHeader()}";
            for (int i = 0; i < content.Count(); i++)
            {
                if (tipoTabla == 1)
                    xml += HeaderContent(content[i], i == 0 || i == 2 || i == 4 ? 3200 : 1100);
                else
                    xml += HeaderContent(content[i], i == 0 ? 3500 : 5000);
                //, i == 2 || i == 4 ? 3200 : 1100
            }

            xml += $" {CloseRowHeader}";
            return xml;
        }

        private string GenerateRows(string[] content, int j = 0)
        {
            string xml = $"{OpenRows()} ";
            for (int i = 0; i < content.Count(); i++)
            { xml += $"{RowContent(content[i], j: j)}"; }
            xml += $" {CloseRowHeader}";
            return xml;
        }

        private string HeaderStyle()
        {
            //w:w=""6000""
            string header = @"
            <w:tbl>
                <w:tblPr>
                    <w:tblW w:w=""6480"" w:type=""auto"" /> 
                    <w:tblInd w:w=""0"" w:type=""dxa"" />
                    <w:tblBorders>
                        <w:top w:val=""nil"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""4F81BD"" />
                        <w:left w:val=""nil"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""4F81BD"" />
                        <w:bottom w:val=""nil"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""4F81BD"" />
                        <w:right w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""04345C"" />
                        <w:insideH w:val=""none"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""auto"" />
                        <w:insideV w:val=""none"" w:sz=""0"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""auto"" />
                    </w:tblBorders>
                    <w:tblCellMar>
                        <w:top w:w=""0"" w:type=""dxa"" />
                        <w:left w:w=""0"" w:type=""dxa"" />
                        <w:bottom w:w=""0"" w:type=""dxa"" />
                        <w:right w:w=""0"" w:type=""dxa"" />
                    </w:tblCellMar>
                </w:tblPr>
                <w:tblGrid />
            ";
            return header;
        }

        private string OpenHeader(int height = 482)
        {
            return
                $@"
                <w:tr>
                    <w:trPr>
                        <w:trHeight w:hRule=""at-least"" w:val=""{height}"" />
                    </w:trPr>
               ";
        }

        private string HeaderContent(string value, int width = 5000)
        {
            return
            $@"
            <w:tc>
                <w:tcPr>
                    <w:tcW w:w=""{width}"" w:type=""dxa"" />
                    <w:tcBorders>
                        <w:top w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""4F81BD"" />
                        <w:left w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""4F81BD"" />
                        <w:bottom w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""FFFFFF"" />
                        <w:right w:val=""nil"" />
                    </w:tcBorders>
                    <w:shd w:val=""clear"" w:color=""auto"" w:fill=""4F81BD"" />
                    <w:tcMar>
                        <w:top w:w=""0"" w:type=""dxa"" />
                        <w:left w:w=""108"" w:type=""dxa"" />
                        <w:bottom w:w=""0"" w:type=""dxa"" />
                        <w:right w:w=""108"" w:type=""dxa"" />
                    </w:tcMar>
                    <w:vAlign w:val=""center"" />
                    <w:hideMark />
                </w:tcPr>
            <w:p>
                <w:pPr>
                    <w:shd w:val=""clear"" w:fill=""4F81BD"" />
                    <w:spacing w:before=""0"" w:after=""0"" />
                    <w:ind w:first-line=""0"" w:left=""0"" w:right=""0"" />
                    <w:jc w:val=""center"" />
                    <w:rPr>
                        <w:shd w:val=""clear"" w:color=""auto"" w:fill=""4F81BD"" />
                    </w:rPr>
                </w:pPr>
                <w:r>
                    <w:rPr>
                        <w:rFonts w:ascii=""Arial"" w:h-ansi=""Arial"" w:cs=""Arial"" w:fareast=""Arial"" />
                        <w:b w:val=""on"" />
                        <w:i w:val=""off"" />
                        <w:color w:val=""FFFFFF"" />
                        <w:sz w:val=""20"" />
                        <w:sz-cs w:val=""20"" />
                    </w:rPr>
                    <w:t>{value}</w:t>
                </w:r>
            </w:p>
        </w:tc>    
            ";
        } //1600

        private string CloseRowHeader { get { return @"</w:tr>"; } }

        private string CloseTable { get { return @"</w:tbl>"; } }

        private string OpenRows(int height = 200)
        {
            return
                $@"
                <w:tr>
                    <w:trPr>
                        <w:trHeight w:hRule=""at-least"" w:val=""{height}"" />
                    </w:trPr>
               ";
        }

        private string RowContent(string value, int width = 1600, int j = 0)
        {
            string stilo1 = j % 2 == 0 ? "DCDCDC" : "FFFFFF";
            stilo1 = "B8CCE4";
            return $@"
            <w:tc>
                <w:tcPr>
                    <w:tcW w:w=""{width}"" w:type=""dxa"" />
                    <w:tcBorders>
                        <w:top w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""FFFFFF"" />
                        <w:left w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""FFFFFF"" />
                        <w:bottom w:val=""single"" w:sz=""8"" w:space=""0"" w:shadow=""off"" w:frame=""off"" w:color=""FFFFFF"" />
                        <w:right w:val=""nil"" />
                    </w:tcBorders>
                    <w:shd w:val=""clear"" w:color=""auto"" w:fill=""{stilo1}"" />
                    <w:tcMar>
                        <w:top w:w=""0"" w:type=""dxa"" />
                        <w:left w:w=""108"" w:type=""dxa"" />
                        <w:bottom w:w=""0"" w:type=""dxa"" />
                        <w:right w:w=""108"" w:type=""dxa"" />
                    </w:tcMar>
                    <w:vAlign w:val=""top"" />
                    <w:hideMark />
                </w:tcPr>
                <w:p>
                    <w:pPr>
                        <w:shd w:val=""clear"" w:fill=""{stilo1}"" />
                        <w:spacing w:before=""100"" w:after=""100"" />
                        <w:ind w:first-line=""0"" w:left=""0"" w:right=""0"" />
                        <w:jc w:val=""center"" />
                        <w:rPr>
                            <w:shd w:val=""clear"" w:color=""auto"" w:fill=""{stilo1}"" />
                        </w:rPr>
                    </w:pPr>
                    <w:r>
                        <w:rPr>
                            <w:rFonts w:ascii=""Arial"" w:h-ansi=""Arial"" w:cs=""Arial"" w:fareast=""Arial"" />
                            <w:b w:val=""off"" />
                            <w:i w:val=""off"" />
                            <w:color w:val=""000000"" />
                            <w:sz w:val=""20"" />
                            <w:sz-cs w:val=""20"" />
                        </w:rPr>
                        <w:t>{value}</w:t>
                    </w:r>
                </w:p>
            </w:tc>      
            ";
        } //160

        private string GetParamImagesOld()
        {
            try
            {
                if (template != null)
                {
                    int nPosA3 = -1; int nPosB3 = -1; int nPosC3 = -1;
                    if (nPosA3 == -1) { nPosA3 = template.IndexOf("ANEXO N° 1"); sApertura3 = "<w:p><w:"; }
                    if (nPosB3 == -1) { nPosB3 = template.IndexOf("ANEXO N° 1"); sCierre3 = "</w:pict></w:r></w:p>"; }
                    string textP3 = template.Substring(nPosA3, (nPosB3 - nPosA3) + sCierre3.Length + 999999);

                    if (nPosA3 > 1) { nPosA3 = textP3.IndexOf("<w:p><w:"); sApertura3 = "<w:p><w:"; }
                    if (nPosB3 > 1) { nPosB3 = textP3.IndexOf("</w:pict></w:r></w:p>"); sCierre3 = "</w:pict></w:r></w:p>"; }

                    XMLIMAGEN = textP3.Substring(nPosA3, (nPosB3 - nPosA3) + sCierre3.Length);

                    return XMLIMAGEN;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Obtener Parámetros");
                return null;
            }
        }

        private string GetParamImagesNew(string @params)
        {
            try
            {
                int nPosA2 = -1; int nPosB2 = -1; int nPosC2 = -1;
                if (nPosA2 == -1) { nPosA2 = @params.IndexOf("<w:p><w:"); sApertura2 = "<w:p><w:"; }
                if (nPosB2 == -1) { nPosB2 = @params.IndexOf("</w:pict></w:r></w:p>"); sCierre2 = "</w:pict></w:r></w:p>"; }
                string textP2 = @params.Substring(nPosA2, (nPosB2 - nPosA2) + sCierre2.Length);
                if (nPosA2 > 1) { nPosA2 = textP2.IndexOf("<w:p><w:"); sApertura2 = "<w:p><w:"; }
                if (nPosB2 > 1) { nPosB2 = textP2.IndexOf("</w:pict></w:r></w:p>"); sCierre2 = "</w:pict></w:r></w:p>"; }
                txtXMLImagen = textP2.Substring(nPosA2, (nPosB2 - nPosA2) + sCierre2.Length);
                txtXMLImagen = txtXMLImagen.Replace("image1", "imagen200");

                nPosA2 = txtXMLImagen.IndexOf("width:"); sApertura2 = "width:";
                nPosB2 = txtXMLImagen.IndexOf(";height"); sCierre2 = ";height";
                string width = txtXMLImagen.Substring(nPosA2 + sApertura2.Length, (nPosB2 - nPosA2 + 1) - sCierre2.Length);
                txtXMLImagen = txtXMLImagen.Replace(width, "448.5pt");
                return txtXMLImagen;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Obtener Parámetros");
                return null;
            }
        }

        private string RowListContent(string value)
        {
            return $@"
            <w:p>
	            <w:pPr>
		            <w:pStyle w:val=""P4""/>
		            <w:listPr>
			            <w:ilvl w:val=""0""/>
			            <w:ilfo w:val=""7""/>
		            </w:listPr>
		            <w:tabs>
			            <w:tab w:val=""left"" w:pos=""5434"" w:leader=""none""/>
		            </w:tabs>
		            <w:spacing w:line-rule=""auto"" w:line=""259"" w:before-autospacing=""off"" w:after-autospacing=""off""/>
		            <w:ind w:left=""1276""/>
		            <w:jc w:val=""both""/>
		            <w:rPr>
			            <w:rFonts w:ascii=""Arial"" w:h-ansi=""Arial"" w:cs=""Arial""/>
			            <w:b w:val=""on""/>
			            <w:sz w:val=""20""/>
			            <w:sz-cs w:val=""20""/>
		            </w:rPr>
	            </w:pPr>
	            <w:r>
		            <w:rPr>
			            <w:rFonts w:ascii=""Arial"" w:h-ansi=""Arial"" w:cs=""Arial""/>
			            <w:sz w:val=""20""/>
			            <w:sz-cs w:val=""20""/>
		            </w:rPr>
		            <w:t xml:space=""preserve"">{value}</w:t>
	            </w:r>
            </w:p>
                ";
        }
    }
}
