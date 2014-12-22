using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Infragistics.Xamarin.Controls;
using GenCode.DroneDown;
using Infragistics.Xamarin;
using GenCode.Logging;

namespace GenCode.DroneDown
{
	public partial class TabPageMonitor : ContentPage
	{
		private readonly RadialGaugeRange _distanceRange = new RadialGaugeRange ();
		private readonly RadialGaugeRange _rssiRange = new RadialGaugeRange ();
		private readonly Label _bigLabel = new Label ();

		public TabPageMonitor ()
		{
			InitializeComponent ();
			Log.WriteLine ("TabPageMonitor, loading.", TraceLogLevel.Verbose);

			FormatGauges ();
		}


		protected override void OnAppearing ()
		{
			base.OnAppearing ();		

			MessagingCenter.Subscribe <TabPageMonitor, Tuple<bool, int, double>> (this, "BeaconMessage", (sender, messageData) => 
					Device.BeginInvokeOnMainThread (() => {
				bool found = messageData.Item1;
				int rssi = messageData.Item2;
				double distance = messageData.Item3;   

				if (found) {
					// calculate the distance if its been calibrated
					// default to the distance from library distance (aka accuracy)
					distance = DistanceCalculator.Calibrated ? DistanceCalculator.CalculateDistance (rssi) : distance;

					// set the message
					string message = String.Format ("(Rssi) {0} Distance {1:0.0}", rssi, distance);
					BeaconDistanceMessages.Text = message;
					Log.WriteLine (message);
					
					// set the end ranges
					_rssiRange.EndValue = rssi;
					_distanceRange.EndValue = distance * -1.0;

					// set the big label to distance
					string bigLabelText = String.Format ("{0:0.0}", distance);
					_bigLabel.Text = bigLabelText;
					Log.WriteLine(bigLabelText);
				} else {
					_bigLabel.Text = "N/A";
				}
			}));
		}

		void FormatGauges ()
		{
			const int rssiMin = -99;
			const int rssiMax = -33;
			const double distanceMin = -50.0;
			const double distanceMax = 0.0;

			if (_bigLabel != null) {
				var font = Font.OfSize ("Helvetica", 40);
				_bigLabel.Font = font;
			}

			// Outside guage - Distance
			var minorTickBrush = new SolidColorBrush (Color.FromRgb (96, 95, 91));
			radialGauge.MinorTickBrush = minorTickBrush;
			radialGauge.MinorTickCount = 4;
			radialGauge.MinorTickStartExtent = .78;
			radialGauge.MinorTickEndExtent = .81;
			radialGauge.MinorTickStrokeThickness = 3;
			radialGauge.TickStrokeThickness = 3;
			radialGauge.ScaleStartAngle = 90;
			radialGauge.ScaleEndAngle = 357;
			//			radialGauge.MinimumValue = 0;
			//			radialGauge.MaximumValue = 40;
			radialGauge.MinimumValue = distanceMin;
			radialGauge.MaximumValue = distanceMax;
			radialGauge.Interval = 5;
			radialGauge.LabelInterval = 40;
			radialGauge.NeedleBrush = null;
			radialGauge.NeedlePivotBrush = null;
			radialGauge.FontBrush = new SolidColorBrush (Color.Transparent);
			radialGauge.TickBrush = minorTickBrush;
			radialGauge.TickStartExtent = .78;
			radialGauge.TickEndExtent = .81;
			radialGauge.BackingOutline = null;
			radialGauge.NeedleOutline = null;
			radialGauge.NeedlePivotOutline = null;

			radialGauge.BackingBrush = new SolidColorBrush (Color.FromRgba (0, 0, 0, 255));
			radialGauge.NeedlePivotShape = RadialGaugePivotShape.None;
			radialGauge.ScaleBrush = null;

			radialGaugeInner.MinorTickBrush = minorTickBrush;
			radialGaugeInner.MinorTickCount = 3;
			radialGaugeInner.MinorTickStartExtent = .57;
			radialGaugeInner.MinorTickEndExtent = .60;
			radialGaugeInner.MinorTickStrokeThickness = 2;
			radialGaugeInner.TickStrokeThickness = 2;
			radialGaugeInner.ScaleStartAngle = 90;
			radialGaugeInner.ScaleEndAngle = 357;
			radialGaugeInner.MinimumValue = distanceMin;
			radialGaugeInner.MaximumValue = distanceMax;
			radialGaugeInner.Interval = 5;
			radialGaugeInner.LabelInterval = 40;
			radialGaugeInner.NeedleBrush = null;
			radialGaugeInner.NeedlePivotBrush = null;
			radialGaugeInner.FontBrush = new SolidColorBrush (Color.Transparent);
			radialGaugeInner.TickBrush = minorTickBrush;
			radialGaugeInner.TickStartExtent = .57;
			radialGaugeInner.TickEndExtent = .60;
			radialGaugeInner.BackingOutline = null;
			radialGaugeInner.NeedleOutline = null;
			radialGaugeInner.NeedlePivotOutline = null;
			radialGaugeInner.BackingBrush = null;
			radialGaugeInner.NeedlePivotShape = RadialGaugePivotShape.None;
			radialGaugeInner.ScaleBrush = null;

			#region rssiGauge
			var suvsBrush = new SolidColorBrush (Color.FromRgb (0, 255, 20));

			_rssiRange.InnerStartExtent = .57;
			_rssiRange.InnerEndExtent = .57;
			_rssiRange.OuterStartExtent = .60;
			_rssiRange.OuterEndExtent = .60;
			_rssiRange.Brush = suvsBrush;
			_rssiRange.BindingContext = this;
			_rssiRange.StartValue = -90;


			rssiUnderGauge.MinorTickBrush = minorTickBrush;
			rssiUnderGauge.MinorTickCount = 3;
			rssiUnderGauge.MinorTickStartExtent = .37;
			rssiUnderGauge.MinorTickEndExtent = .40;
			rssiUnderGauge.MinorTickStrokeThickness = 1;
			rssiUnderGauge.TickStrokeThickness = 1;
			rssiUnderGauge.ScaleStartAngle = 90;
			rssiUnderGauge.ScaleEndAngle = 357;
			rssiUnderGauge.MinimumValue = rssiMin;
			rssiUnderGauge.MaximumValue = rssiMax;
			rssiUnderGauge.Interval = 5;
			rssiUnderGauge.LabelInterval = 40;
			rssiUnderGauge.NeedleBrush = null;
			rssiUnderGauge.NeedlePivotBrush = null;
			rssiUnderGauge.FontBrush = new SolidColorBrush (Color.Transparent);
			rssiUnderGauge.TickBrush = minorTickBrush;
			rssiUnderGauge.TickStartExtent = .37;
			rssiUnderGauge.TickEndExtent = .40;
			rssiUnderGauge.BackingOutline = null;
			rssiUnderGauge.NeedleOutline = null;
			rssiUnderGauge.NeedlePivotOutline = null;
			rssiUnderGauge.BackingBrush = null;
			rssiUnderGauge.NeedlePivotShape = RadialGaugePivotShape.None;
			rssiUnderGauge.ScaleBrush = null;

			rssiUnderGauge.Ranges.Add (_rssiRange);
			#endregion

			#region distanceGauge
			// RadialGaugeRange
			var tickBrush = new SolidColorBrush (Color.FromRgb (0, 255, 20));
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
			distanceGauge.FontBrush = new SolidColorBrush (Color.Transparent);
			distanceGauge.Font = Font.OfSize ("Helvetica", 14).WithAttributes (FontAttributes.Bold);
			distanceGauge.ScaleStartAngle = 90;
			distanceGauge.ScaleEndAngle = 357;
			distanceGauge.MinimumValue = distanceMin;
			distanceGauge.MaximumValue = distanceMax;
			distanceGauge.Interval = 5;
			distanceGauge.LabelInterval = 10;
			distanceGauge.MinorTickBrush = null;
			distanceGauge.BackingOutline = null;
			distanceGauge.BackingBrush = null;
			distanceGauge.NeedleBrush = null;
			distanceGauge.NeedleOutline = null;
			distanceGauge.NeedlePivotOutline = null;
			distanceGauge.NeedlePivotBrush = null;
			distanceGauge.ScaleBrush = null;
			distanceGauge.NeedlePivotShape = RadialGaugePivotShape.None;
			distanceGauge.BindingContext = this;

			distanceGauge.TickBrush = tickBrush;
			distanceGauge.TickStartExtent = .72;
			distanceGauge.TickEndExtent = .78;
			distanceGauge.LabelExtent = .66;
			distanceGauge.Ranges.Add (_distanceRange);
			#endregion


			_bigLabel.Font = Font.OfSize ("Helvetica", 40);
			_bigLabel.TextColor = Color.FromHex ("B3B3B3");
			_bigLabel.VerticalOptions = LayoutOptions.FillAndExpand;
			_bigLabel.HorizontalOptions = LayoutOptions.FillAndExpand;
			_bigLabel.XAlign = TextAlignment.Center;
			_bigLabel.YAlign = TextAlignment.Center;
			contentGrid.Children.Add (_bigLabel);
			Grid.SetRow (_bigLabel, 1);
		}
	}
}


