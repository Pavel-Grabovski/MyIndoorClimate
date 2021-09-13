using System;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;



namespace MyIndoorClimate.Acoustics
{
    public class Add : IExternalApplication
    {
        string RibbonTab = "MyIndoorClimate";
        string RibbonPanel = "Расчет звукоизоляции";
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                application.CreateRibbonTab(RibbonTab);
            }
            catch (Exception) { }//tab adready exists*/
            // get or create the panet
            RibbonPanel ribbonPanel = null;
            foreach(RibbonPanel pnl in application.GetRibbonPanels(RibbonTab))
            {
                if (pnl.Name == RibbonPanel)
                {
                    ribbonPanel = pnl;
                    break;
                }
            }
            if (ribbonPanel == null)
            {
                ribbonPanel = application.CreateRibbonPanel(RibbonTab, RibbonPanel);
            }
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdAcoustics", "Расчет \n звукоизоляции",
                thisAssemblyPath, "MyIndoorClimate.Acoustics.Command");

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            // При желании кнопке могут быть присвоены другие свойства.
            // a) Подсказка
            pushButton.ToolTip = "Расчет звукоизоляции стен и перекрытий";
            
            Image ImageRebarSquareColumns = global::MyIndoorClimate.Properties.Resources.pngwing;
            ImageSource _imageSoursRebarSquareColumns = GetImageSourse(ImageRebarSquareColumns);
            pushButton.LargeImage = _imageSoursRebarSquareColumns;

            


            return Result.Succeeded;
        }
        private ImageSource GetImageSourse(Image img)
        {
            BitmapImage bmp = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Position = 0;

                bmp.BeginInit();

                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = null;
                bmp.StreamSource = ms;

                bmp.EndInit();
            }
            return bmp;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
