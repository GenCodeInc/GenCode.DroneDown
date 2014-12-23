using System;
using GenCode.Logging;
using Infragistics.Xamarin;
using Infragistics.Xamarin.Controls;
using Xamarin.Forms;

namespace GenCode.DroneDown.Views
{
	public sealed partial class TabPageMonitor
	{
        private readonly RadialGaugeRange _distanceRange = new RadialGaugeRange();
        public RadialGaugeRange DistanceRange
        {
            get { return _distanceRange; }
        }

        private RadialGaugeRange _rssiRange = new RadialGaugeRange();
        public RadialGaugeRange RssiRange
	    {
	        get { return _rssiRange; }
	        set { _rssiRange = value; }
	    }

        private readonly Label _bigLabel = new Label();
        public Label BigLabel
        {
            get { return _bigLabel; }
        }

		public TabPageMonitor ()
		{
			InitializeComponent ();
			Log.WriteLine ("TabPageMonitor, loading.", TraceLogLevel.Verbose);

			SetupGauges ();
		}


	    /// <summary>
	    /// When overridden, allows application developers to customize behavior immediately prior to the becoming visible.
	    /// </summary>
	    protected override void OnAppearing ()
		{
			base.OnAppearing ();		

			MessagingCenter.Subscribe <TabPageMonitor, Tuple<bool, int, double>> (this, "BeaconMessage", (sender, messageData) =>
                    Device.BeginInvokeOnMainThread(() => {
                        Log.WriteLine("ProcessBeaconMessage to UI"); 
                        ProcessBeaconMessage(messageData.Item1, messageData.Item2, messageData.Item3); }));
		}

	    /// <summary>
	    /// Every beacon tick sends a message center message, this method will take that data and display it on the graph
	    /// </summary>
	    /// <param name="found"></param>
	    /// <param name="rssi"></param>
	    /// <param name="distance"></param>
	    private void ProcessBeaconMessage(bool found, int rssi, double distance)
	    {
	        if (found)
	        {
	            // calculate the distance if its been calibrated
	            // default to the distance from library distance (aka accuracy)
	            distance = DistanceCalculator.Calibrated ? DistanceCalculator.CalculateDistance(rssi) : distance;

	            // set the message
	            var message = String.Format("(Rssi) {0} Distance {1:0.0}", rssi, distance);
	            BeaconDistanceMessages.Text = message;
	            Log.WriteLine(message);

	            // set the end ranges
	            _rssiRange.EndValue = rssi;
	            _distanceRange.EndValue = distance*-1.0;

	            // set the big label to distance
                var bigLabelText = String.Format("{0:0.0}", distance);
	            _bigLabel.Text = bigLabelText;
	            Log.WriteLine(bigLabelText);
	        }
	        else
	        {
	            _bigLabel.Text = "N/A";
	        }
	    }

        /// <summary>
        /// SetupGauges is the main method that sets up the look of the Infragistic gauges
        /// 
        /// </summary>
	    void SetupGauges ()
		{
			const int rssiMin = -99;
			const int rssiMax = -33;
			const double distanceMin = -50.0;
			const double distanceMax = 0.0;
            
            // get minor tick color
            var minorTickBrush = new SolidColorBrush(Color.FromRgb(96, 95, 91));

            DistanceOutterGuage(minorTickBrush, distanceMin, distanceMax);
			RssiGauge(minorTickBrush, rssiMin, rssiMax);
	        DistanceInnerGauge(distanceMin, distanceMax);

            ContentGrid.Children.Add (_bigLabel);
	        Grid.SetRow (_bigLabel, 1);
		}


        /// <summary>
        /// Set up the inner gauge 
        /// </summary>
        /// <param name="distanceMin"></param>
        /// <param name="distanceMax"></param>
	    private void DistanceInnerGauge(double distanceMin, double distanceMax)
	    {
	        #region distanceGauge

	        // RadialGaugeRange
	        var tickBrush = new SolidColorBrush(Color.FromRgb(0, 255, 20));
	        _distanceRange.InnerStartExtent = .77;
	        _distanceRange.InnerEndExtent = .77;
	        _distanceRange.OuterStartExtent = .81;
	        _distanceRange.OuterEndExtent = .81;
	        _distanceRange.Brush = tickBrush;
	        _distanceRange.BindingContext = this;
	        //			DistanceRange.StartValue = -.25;
	        _distanceRange.StartValue = distanceMin;

	        // RadialGauge
	        // this is the dial that goes around the outside printing the numbers
	        //gauge.FontBrush = new SolidColorBrush (Color.FromHex ("B3B3B3"));
	        DistanceGauge.FontBrush = new SolidColorBrush(Color.Transparent);
	        DistanceGauge.Font = Font.OfSize("Helvetica", 14).WithAttributes(FontAttributes.Bold);
	        DistanceGauge.ScaleStartAngle = 90;
	        DistanceGauge.ScaleEndAngle = 357;
	        DistanceGauge.MinimumValue = distanceMin;
	        DistanceGauge.MaximumValue = distanceMax;
	        DistanceGauge.Interval = 5;
	        DistanceGauge.LabelInterval = 10;
	        DistanceGauge.MinorTickBrush = null;
	        DistanceGauge.BackingOutline = null;
	        DistanceGauge.BackingBrush = null;
	        DistanceGauge.NeedleBrush = null;
	        DistanceGauge.NeedleOutline = null;
	        DistanceGauge.NeedlePivotOutline = null;
	        DistanceGauge.NeedlePivotBrush = null;
	        DistanceGauge.ScaleBrush = null;
	        DistanceGauge.NeedlePivotShape = RadialGaugePivotShape.None;
	        DistanceGauge.BindingContext = this;

	        DistanceGauge.TickBrush = tickBrush;
	        DistanceGauge.TickStartExtent = .72;
	        DistanceGauge.TickEndExtent = .78;
	        DistanceGauge.LabelExtent = .66;
	        DistanceGauge.Ranges.Add(_distanceRange);

	        #endregion
	    }

        /// <summary>
        /// Setup the RSSI gauge
        /// </summary>
        /// <param name="minorTickBrush"></param>
        /// <param name="rssiMin"></param>
        /// <param name="rssiMax"></param>
	    private void RssiGauge(SolidColorBrush minorTickBrush, int rssiMin, int rssiMax)
	    {
	        #region rssiGauge

	        var suvsBrush = new SolidColorBrush(Color.FromRgb(0, 255, 20));

	        _rssiRange.InnerStartExtent = .57;
	        _rssiRange.InnerEndExtent = .57;
	        _rssiRange.OuterStartExtent = .60;
	        _rssiRange.OuterEndExtent = .60;
	        _rssiRange.Brush = suvsBrush;
	        _rssiRange.BindingContext = this;
	        _rssiRange.StartValue = -90;


	        RssiUnderGauge.MinorTickBrush = minorTickBrush;
	        RssiUnderGauge.MinorTickCount = 3;
	        RssiUnderGauge.MinorTickStartExtent = .37;
	        RssiUnderGauge.MinorTickEndExtent = .40;
	        RssiUnderGauge.MinorTickStrokeThickness = 1;
	        RssiUnderGauge.TickStrokeThickness = 1;
	        RssiUnderGauge.ScaleStartAngle = 90;
	        RssiUnderGauge.ScaleEndAngle = 357;
	        RssiUnderGauge.MinimumValue = rssiMin;
	        RssiUnderGauge.MaximumValue = rssiMax;
	        RssiUnderGauge.Interval = 5;
	        RssiUnderGauge.LabelInterval = 40;
	        RssiUnderGauge.NeedleBrush = null;
	        RssiUnderGauge.NeedlePivotBrush = null;
	        RssiUnderGauge.FontBrush = new SolidColorBrush(Color.Transparent);
	        RssiUnderGauge.TickBrush = minorTickBrush;
	        RssiUnderGauge.TickStartExtent = .37;
	        RssiUnderGauge.TickEndExtent = .40;
	        RssiUnderGauge.BackingOutline = null;
	        RssiUnderGauge.NeedleOutline = null;
	        RssiUnderGauge.NeedlePivotOutline = null;
	        RssiUnderGauge.BackingBrush = null;
	        RssiUnderGauge.NeedlePivotShape = RadialGaugePivotShape.None;
	        RssiUnderGauge.ScaleBrush = null;

	        RssiUnderGauge.Ranges.Add(_rssiRange);

	        #endregion
	    }

        /// <summary>
        /// Setup Outside guage - Distance
        /// </summary>
        /// <param name="minorTickBrush"></param>
        /// <param name="distanceMin"></param>
        /// <param name="distanceMax"></param>
        private void DistanceOutterGuage(SolidColorBrush minorTickBrush,  double distanceMin, double distanceMax)
	    {
            RadialGauge.MinorTickBrush = minorTickBrush;
	        RadialGauge.MinorTickCount = 4;
	        RadialGauge.MinorTickStartExtent = .78;
	        RadialGauge.MinorTickEndExtent = .81;
	        RadialGauge.MinorTickStrokeThickness = 3;
	        RadialGauge.TickStrokeThickness = 3;
	        RadialGauge.ScaleStartAngle = 90;
	        RadialGauge.ScaleEndAngle = 357;
	        RadialGauge.MinimumValue = distanceMin;
	        RadialGauge.MaximumValue = distanceMax;
	        RadialGauge.Interval = 5;
	        RadialGauge.LabelInterval = 40;
	        RadialGauge.NeedleBrush = null;
	        RadialGauge.NeedlePivotBrush = null;
	        RadialGauge.FontBrush = new SolidColorBrush(Color.Transparent);
	        RadialGauge.TickBrush = minorTickBrush;
	        RadialGauge.TickStartExtent = .78;
	        RadialGauge.TickEndExtent = .81;
	        RadialGauge.BackingOutline = null;
	        RadialGauge.NeedleOutline = null;
	        RadialGauge.NeedlePivotOutline = null;

	        RadialGauge.BackingBrush = new SolidColorBrush(Color.FromRgba(0, 0, 0, 255));
	        RadialGauge.NeedlePivotShape = RadialGaugePivotShape.None;
	        RadialGauge.ScaleBrush = null;

            RadialGaugeInner.MinorTickBrush = minorTickBrush;
            RadialGaugeInner.MinorTickCount = 3;
            RadialGaugeInner.MinorTickStartExtent = .57;
            RadialGaugeInner.MinorTickEndExtent = .60;
            RadialGaugeInner.MinorTickStrokeThickness = 2;
            RadialGaugeInner.TickStrokeThickness = 2;
            RadialGaugeInner.ScaleStartAngle = 90;
            RadialGaugeInner.ScaleEndAngle = 357;
            RadialGaugeInner.MinimumValue = distanceMin;
            RadialGaugeInner.MaximumValue = distanceMax;
            RadialGaugeInner.Interval = 5;
            RadialGaugeInner.LabelInterval = 40;
            RadialGaugeInner.NeedleBrush = null;
            RadialGaugeInner.NeedlePivotBrush = null;
            RadialGaugeInner.FontBrush = new SolidColorBrush(Color.Transparent);
            RadialGaugeInner.TickBrush = minorTickBrush;
            RadialGaugeInner.TickStartExtent = .57;
            RadialGaugeInner.TickEndExtent = .60;
            RadialGaugeInner.BackingOutline = null;
            RadialGaugeInner.NeedleOutline = null;
            RadialGaugeInner.NeedlePivotOutline = null;
            RadialGaugeInner.BackingBrush = null;
            RadialGaugeInner.NeedlePivotShape = RadialGaugePivotShape.None;
            RadialGaugeInner.ScaleBrush = null;
	    }
	}
}


