using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Microsoft.Samples.KMoore.WPFSamples.AnimatingTilePanel
{
    public class AniTilePanel : Panel
    {
        private const double Diff = 0.1;
        private readonly EventHandler CompositionTarget_RenderingHandler;
        private long _lastTick = long.MinValue;

        public AniTilePanel()
        {
            CompositionTarget_RenderingHandler = CompositionTarget_Rendering;

            //need to make sure we only run the ticker when the control is actually loaded            
            Loaded += AniTilePanel_Loaded;
            Unloaded += AniTilePanel_Unloaded;
        }

        private void AniTilePanel_Loaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering += CompositionTarget_RenderingHandler;
        }

        private void AniTilePanel_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_RenderingHandler;
        }

        // Measures the children
        protected override Size MeasureOverride(Size availableSize)
        {
            /*
            Size theChildSize = new Size(ItemWidth, ItemHeight);

            foreach (UIElement child in Children)
            {
                child.Measure(theChildSize);
            }*/

            int childrenPerRow;

            // Figure out how many children fit on each row
            if (availableSize.Width == Double.PositiveInfinity)
            {
                childrenPerRow = Children.Count;
            }
            else
            {
                childrenPerRow = Math.Max(1, (int) Math.Floor(availableSize.Width / ItemWidth));
            }

            // Calculate the width and height this results in
            double width = childrenPerRow * ItemWidth;
            double height = ItemHeight * (Math.Ceiling((double) Children.Count / childrenPerRow));
            return new Size(width, height);
        }

        // Arrange the children
        protected override Size ArrangeOverride(Size finalSize)
        {
            // Calculate how many children fit on each row
            int childrenPerRow = Math.Max(1, (int) Math.Floor(finalSize.Width / ItemWidth));

            Size theChildSize = new Size(ItemWidth, ItemHeight);

            double itemWidth = ItemWidth;
            double itemHeight = ItemHeight;

            for (int i = 0; i < Children.Count; i++)
            {
                UIElement child = Children[i];

                // Figure out where the child goes
                Point newOffset =
                    CalcChildOffset(i, childrenPerRow, itemWidth, itemHeight, finalSize.Width);

                //set the location attached DP
                child.SetValue(ChildTargetProperty, newOffset);

                if (child.ReadLocalValue(ChildLocationProperty) == DependencyProperty.UnsetValue)
                {
                    child.SetValue(ChildLocationProperty, newOffset);
                    child.Arrange(new Rect(newOffset, theChildSize));
                }
                else
                {
                    Point currentOffset = (Point) child.GetValue(ChildLocationProperty);
                    // Position the child and set its size
                    child.Arrange(new Rect(currentOffset, theChildSize));
                }
            }
            return finalSize;
        }

        #region public properties

        // Using a DependencyProperty as the backing store for ItemWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AttractionProperty = DependencyProperty.Register(
            "Attraction", typeof (double), typeof (AniTilePanel),
            new FrameworkPropertyMetadata((double) 2));

        public static readonly DependencyProperty DampeningProperty = DependencyProperty.Register(
            "Dampening", typeof (double), typeof (AniTilePanel),
            new FrameworkPropertyMetadata(.8));

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof (double), typeof (AniTilePanel),
                                        new FrameworkPropertyMetadata((double) 50,
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof (double), typeof (AniTilePanel),
                                        new FrameworkPropertyMetadata((double) 50,
                                                                      FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ItemWidth
        {
            get
            {
                return (double) GetValue(ItemWidthProperty);
            }
            set
            {
                SetValue(ItemWidthProperty, value);
            }
        }

        public double ItemHeight
        {
            get
            {
                return (double) GetValue(ItemHeightProperty);
            }
            set
            {
                SetValue(ItemHeightProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for ItemHeight.  This enables animation, styling, binding, etc...

        public double Dampening
        {
            get
            {
                return (double) GetValue(DampeningProperty);
            }
            set
            {
                SetValue(DampeningProperty, value);
            }
        }

        public double Attraction
        {
            get
            {
                return (double) GetValue(AttractionProperty);
            }
            set
            {
                SetValue(AttractionProperty, value);
            }
        }

        #endregion

        #region private attached properties

        private static readonly DependencyProperty ChildLocationProperty
            = DependencyProperty.RegisterAttached("ChildLocation", typeof (Point), typeof (AniTilePanel)
                                                  ,
                                                  new FrameworkPropertyMetadata(new Point(),
                                                                                FrameworkPropertyMetadataOptions.
                                                                                    AffectsParentArrange));

        private static readonly DependencyProperty ChildTargetProperty
            = DependencyProperty.RegisterAttached("ChildTarget", typeof (Point), typeof (AniTilePanel));

        private static readonly DependencyProperty VelocityProperty
            = DependencyProperty.RegisterAttached("Velocity", typeof (Vector), typeof (AniTilePanel));

        public static void SetChildLocation(UIElement element, Point point)
        {
            element.SetValue(ChildLocationProperty, point);
        }

        public static Point GetChildLocation(UIElement element)
        {
            return (Point) element.GetValue(ChildLocationProperty);
        }

        public static void SetChildTarget(UIElement element, Point point)
        {
            element.SetValue(ChildTargetProperty, point);
        }

        public static Point GetChildTarget(UIElement element)
        {
            return (Point) element.GetValue(ChildTargetProperty);
        }

        public static void SetVelocity(UIElement element, Vector Vector)
        {
            element.SetValue(VelocityProperty, Vector);
        }

        public static Vector GetVelocity(UIElement element)
        {
            return (Vector) element.GetValue(VelocityProperty);
        }

        #endregion

        #region private methods

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            long nowTick = DateTime.Now.Ticks;
            long diff = nowTick - _lastTick;

            _lastTick = nowTick;

            double seconds = diff / 10000000.0; //1 tick = 100-nanoseconds, so 10,000,000

            double dampening = Dampening;
            double attractionFactor = Attraction;

            foreach (UIElement child in Children)
            {
                updateElement(child, seconds, dampening, attractionFactor);
            }
        }

        private static void updateElement(UIElement element, double seconds, double dampening, double attractionFactor)
        {
            Point current = (Point) element.GetValue(ChildLocationProperty);
            Point target = (Point) element.GetValue(ChildTargetProperty);
            Vector velocity = (Vector) element.GetValue(VelocityProperty);

            Vector diff = target - current;

            if (diff.Length > Diff || velocity.Length > Diff)
            {
                velocity.X *= dampening;
                velocity.Y *= dampening;

                velocity += diff;

                Vector delta = velocity * seconds * attractionFactor;

                //velocity shouldn't be greater than...maxVelocity?
                const double maxVelocity = 100;
                delta *= (delta.Length > maxVelocity) ? (maxVelocity / delta.Length) : 1;

                current += delta;

                element.SetValue(ChildLocationProperty, current);
                element.SetValue(VelocityProperty, velocity);
            }
        }

        // Given a child index, child size and children per row, figure out where the child goes
        private static Point CalcChildOffset(int index, int childrenPerRow, double itemWidth, double itemHeight,
                                             double panelWidth)
        {
            double fudge = (panelWidth - childrenPerRow * itemWidth) / childrenPerRow;
            int row = index / childrenPerRow;
            int column = index % childrenPerRow;
            return new Point(0.5 * fudge + column * (itemWidth + fudge), row * itemHeight);
        }

        #endregion
    }
}