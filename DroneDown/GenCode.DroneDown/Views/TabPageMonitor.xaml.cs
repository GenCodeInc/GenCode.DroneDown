using System;
using GenCode.Logging;
using Xamarin.Forms;

namespace GenCode.DroneDown.Views
{
	public sealed partial class TabPageMonitor
	{
        /// <summary>
        /// The Monitor is used as a graphical representation
        /// </summary>
		public TabPageMonitor ()
		{
		    InitializeComponent ();

            Log.WriteLine("TabPageMonitor, loading", TraceLogLevel.Verbose);

            SetupGauges ();
        }

        /// <summary>
        /// Setup any extra things that I did not do in xaml for reasons that it does not seem to be working in
        /// For example, can not seem to set some attribues to null using {x:Null} in the markup, so doing it here instead
        /// </summary>
	    void SetupGauges ()
		{
            // null out some attribues I dont want to see on dial
            DistanceOuterRadialGaugeInit();
            RssiOuterRadialGaugeInit();
            RssiRadialGaugeRangeInit();
	        DistanceRadialGaugeRangeInit();

            // add a little more binding to the VM
            RssiBarRadialGaugeRange.BindingContext = TabPageMonitorViewModel;
            DistanceBarRadialGaugeRange.BindingContext = TabPageMonitorViewModel;
		}

        /// <summary>
        /// Null out
        /// </summary>
	    private void DistanceRadialGaugeRangeInit()
	    {
	        DistanceRadialGauge.MinorTickBrush = null;
	        DistanceRadialGauge.BackingOutline = null;
	        DistanceRadialGauge.BackingBrush = null;
	        DistanceRadialGauge.NeedleBrush = null;
	        DistanceRadialGauge.NeedleOutline = null;
	        DistanceRadialGauge.NeedlePivotOutline = null;
	        DistanceRadialGauge.NeedlePivotBrush = null;
	        DistanceRadialGauge.ScaleBrush = null;
	    }

        /// <summary>
        /// Setup the RSSI gauge
        /// </summary>
	    private void RssiRadialGaugeRangeInit()
	    {
	        RssiRadialGauge.NeedleBrush = null;
	        RssiRadialGauge.NeedlePivotBrush = null;
	        RssiRadialGauge.BackingOutline = null;
	        RssiRadialGauge.NeedleOutline = null;
	        RssiRadialGauge.NeedlePivotOutline = null;
	        RssiRadialGauge.BackingBrush = null;
	        RssiRadialGauge.ScaleBrush = null;
	    }

        /// <summary>
        /// Setup Outside guage - Distance
        /// </summary>
        private void DistanceOuterRadialGaugeInit()
	    {
            DistanceOuterRadialGauge.NeedleBrush = null;
	        DistanceOuterRadialGauge.NeedlePivotBrush = null;
	        DistanceOuterRadialGauge.BackingOutline = null;
	        DistanceOuterRadialGauge.NeedleOutline = null;
	        DistanceOuterRadialGauge.NeedlePivotOutline = null;
	        DistanceOuterRadialGauge.ScaleBrush = null;
	    }

        /// <summary>
        /// Setup Rssi Outer Gauge
        /// </summary>
	    private void RssiOuterRadialGaugeInit()
	    {
	        RssiOuterRadialGauge.NeedleBrush = null;
	        RssiOuterRadialGauge.NeedlePivotBrush = null;
	        RssiOuterRadialGauge.BackingOutline = null;
	        RssiOuterRadialGauge.NeedleOutline = null;
	        RssiOuterRadialGauge.NeedlePivotOutline = null;
	        RssiOuterRadialGauge.BackingBrush = null;
	        RssiOuterRadialGauge.ScaleBrush = null;
	    }
	}
}


