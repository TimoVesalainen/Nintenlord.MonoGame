namespace Nintenlord.MonoGame.Randoms
{
    public class WorleyNoise
    {
        /*
         
    public class EffectPlugin : PropertyBasedEffect
    {
        private byte instanceSeed = (byte)DateTime.Now.Ticks;


        public static string StaticName
        {
            get
            {
                return "Worley Noise";
            }
        }

        public static Bitmap StaticIcon
        {
            get
            {
                return new Bitmap(typeof(EffectPlugin), "EffectPluginIcon.png");
            }
        }

        public static string StaticSubMenuName
        {
            get
            {
                //return null; // Use for no submenu
                // return "My SubMenu"; // Use for custom submenu
                return SubmenuNames.Render; //Use for predefined submenu
            }
        }

        public EffectPlugin()
            : base(StaticName, StaticIcon, StaticSubMenuName, EffectFlags.Configurable)
        {

        }

        private string metricPropertyName = "Metric to use";
        private string cellSizePropertyName = "Size of a cell";
        private string seedPropertyName = "Seed";

        protected override void OnSetRenderInfo(PropertyBasedEffectConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            //Set render info
            metric = Metrics.GetMetric(
                (Metric)newToken.GetProperty<StaticListChoiceProperty>(metricPropertyName).Value);
            cellSize = newToken.GetProperty<Int32Property>(cellSizePropertyName).Value;

            widthCount = (int)Math.Ceiling((float)dstArgs.Width / (float)cellSize) + 1;
            heigthCount = (int)Math.Ceiling((float)dstArgs.Height / (float)cellSize) + 1;

            SetCenters((byte)newToken.GetProperty<Int32Property>(seedPropertyName).Value);
            
            base.OnSetRenderInfo(newToken, dstArgs, srcArgs);
        }
        
        private void SetCenters(byte seed)
        {
            cellCenters = new Point[widthCount, heigthCount];

            Random rand = new Random(seed ^ instanceSeed);

            for (int j = 0; j < heigthCount; j++)
            {
                for (int i = 0; i < widthCount; i++)
                {
                    int x = rand.Next(cellSize);
                    int y = rand.Next(cellSize);
                    cellCenters[i, j] = new Point(x + cellSize * i, y + cellSize * j);
                }
            }
        }

        protected override PropertyCollection OnCreatePropertyCollection()
        {
            List<Property> properties = new List<Property>();
            properties.Add(new StaticListChoiceProperty(metricPropertyName, Enum.GetValues(typeof(Metric)).Cast<object>().ToArray()));
            properties.Add(new Int32Property(cellSizePropertyName, 5, 1, 1000));
            properties.Add(new Int32Property(seedPropertyName, 0, 0, 0xFF));
            //Add properties
            return new PropertyCollection(properties);
        }

        protected override ControlInfo OnCreateConfigUI(PropertyCollection props)
        {
            ControlInfo configUI = PropertyBasedEffect.CreateDefaultConfigUI(props);
            configUI.SetPropertyControlType(seedPropertyName, PropertyControlType.IncrementButton);
            configUI.SetPropertyControlValue(seedPropertyName, ControlInfoPropertyNames.ButtonText, 
                PdnResources.GetString("CloudsEffect.ConfigDialog.ReseedButton.Text"));

            return configUI;
        }

        int cellSize;
        Func<Point, Point, double> metric;
        int widthCount;
        int heigthCount;
        Point[,] cellCenters;
        
#if DEBUG
        StringBuilder errorLog = new StringBuilder();

        protected override void OnDispose(bool disposing)
        {
            if (errorLog.Length > 0)
            {
                System.IO.File.AppendAllText(@"C:\Users\Timo\Desktop\test.log", errorLog.ToString());
                errorLog.Remove(0, errorLog.Length);
            }
            base.OnDispose(disposing);
        }
#endif


        protected override void OnRender(Rectangle[] renderRects, int startIndex, int length)
        {
            Surface dest = this.DstArgs.Surface;
            Surface src = this.SrcArgs.Surface;

            double maxD = metric(Point.Empty, new Point(cellSize));

            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Rectangle rect = renderRects[i];

                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    int cellY = y / cellSize;
                    for (int x = rect.Left; x < rect.Right; ++x)
                    {
                        int cellX = x / cellSize;

                        double d = GetClosestDistance(x, y, cellX, cellY);
                        ColorBgra destColor = ColorBgra.Lerp(
                            this.EnvironmentParameters.PrimaryColor, 
                            this.EnvironmentParameters.SecondaryColor,
                            d / maxD);
                        dest[x, y] = destColor;
                    }
                }
            }
        }

        private double GetClosestDistance(int x, int y, int cellX, int cellY)
        {
            Point current = new Point(x, y);

            bool hasCellLeft = cellX > 0;
            bool hasCellRight = cellX < widthCount;

            bool hasCellTop = cellY > 0;
            bool hasCellBottom = cellY < heigthCount;

            double d = metric(current, cellCenters[cellX, cellY]);
#if DEBUG
            try
            {
#endif
                if (hasCellLeft)
                {
                    d = Math.Min(d, metric(current, cellCenters[cellX - 1, cellY]));
                    if (hasCellTop)
                        d = Math.Min(d, metric(current, cellCenters[cellX - 1, cellY - 1]));
                    if (hasCellBottom)
                        d = Math.Min(d, metric(current, cellCenters[cellX - 1, cellY + 1]));
                }

                if (hasCellRight)
                {
                    d = Math.Min(d, metric(current, cellCenters[cellX + 1, cellY]));
                    if (hasCellTop)
                        d = Math.Min(d, metric(current, cellCenters[cellX + 1, cellY - 1]));
                    if (hasCellBottom)
                        d = Math.Min(d, metric(current, cellCenters[cellX + 1, cellY + 1]));
                }

                if (hasCellTop)
                    d = Math.Min(d, metric(current, cellCenters[cellX, cellY - 1]));
                if (hasCellBottom)
                    d = Math.Min(d, metric(current, cellCenters[cellX, cellY + 1]));
#if DEBUG
            }
            catch (IndexOutOfRangeException e)
            {
                errorLog.AppendLine(
                    x.ToString() + " " + y.ToString() + ", " + 
                    cellX.ToString() + " " + cellY.ToString() + 
                    " " + e.Message);
            }
#endif

                return d;
        }

    }
         
         
         */
    }
}