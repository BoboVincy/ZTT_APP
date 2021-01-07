using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ZdfFlatUI
{
    /// <summary>
    /// 刻度盘控件
    /// </summary>
    /// <remarks>add by min 2018.2.28</remarks>
    [TemplatePart(Name = "PART_IncreaseCircle", Type = typeof(Arc))]
    public class PalletizingDashboard : Control
    {
        private Arc PART_IncreaseCircle;
        /// <summary>
        /// 保存角度变化前的角度值(用于动画)
        /// </summary>
        private double OldAngle;

        #region Constructors
        static PalletizingDashboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PalletizingDashboard), new FrameworkPropertyMetadata(typeof(PalletizingDashboard)));
        }
        #endregion

        #region 依赖属性

        #region Angle 刻度盘起始角度
        /// <summary>
        /// 刻度盘起始角度依赖属性
        /// </summary>
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(
                "StartAngle",
                typeof(double),
                typeof(PalletizingDashboard),
                new PropertyMetadata(0d));

        /// <summary>
        /// 刻度盘起始角度
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }
        #endregion

        #region Angle 刻度盘结束角度依赖属性
        /// <summary>
        /// 刻度盘结束角度依赖属性
        /// </summary>
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register(
                "EndAngle",
                typeof(double),
                typeof(PalletizingDashboard),
                new PropertyMetadata(0d));

        /// <summary>
        /// 刻度盘结束角度依赖属性
        /// </summary>
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }
        #endregion

        #region Minimum 最小值
        /// <summary>
        /// 最小值依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum",
                typeof(double),
                typeof(PalletizingDashboard),
                new PropertyMetadata(0.0));

        /// <summary>
        /// 获取或设置最小值.
        /// </summary>
        /// <value>最小值.</value>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        #endregion

        #region Maximum 最大值
        /// <summary>
        /// 最大值依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum",
                typeof(double),
                typeof(PalletizingDashboard),
                new PropertyMetadata(100.0));

        /// <summary>
        /// 获取或设置最大值.
        /// </summary>
        /// <value>最大值.</value>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        #endregion

        #region Value 当前值
        /// <summary>
        /// 最大值依赖属性,用于Binding
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(PalletizingDashboard),
                new PropertyMetadata(0.0, new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        /// 获取或设置当前值
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PalletizingDashboard dashboard = d as PalletizingDashboard;
            dashboard.OldAngle = dashboard.Angle;
            dashboard.SetAngle();
            dashboard.TransformAngle();
        }
        #endregion

        #region TickDurtion 刻度改变时的动画显示时长
        public static readonly DependencyProperty TickDurtionProperty = DependencyProperty.Register("TickDurtion"
            , typeof(Duration)
            , typeof(PalletizingDashboard),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(400))));

        /// <summary>
        /// 刻度改变时的动画显示时长
        /// </summary>
        public Duration TickDurtion
        {
            get { return (Duration)GetValue(TickDurtionProperty); }
            set { SetValue(TickDurtionProperty, value); }
        }
        #endregion

        #region Content
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(PalletizingDashboard));
        #endregion

        #region ContentTemplate
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(PalletizingDashboard));
        #endregion

        #endregion

        #region Private依赖属性

        #region Angle 刻度盘当前值所对应的角度
        /// <summary>
        /// 刻度盘当前值所对应的角度依赖属性
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(
                "Angle",
                typeof(double),
                typeof(PalletizingDashboard),
                new PropertyMetadata(0d));

        /// <summary>
        /// 刻度盘当前值所对应的角度
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            private set { SetValue(AngleProperty, value); }
        }
        #endregion

        #endregion

        #region 重载
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_IncreaseCircle = GetTemplateChild("PART_IncreaseCircle") as Arc;

            this.SetAngle();
            this.TransformAngle();
        }
        #endregion

        #region Private方法
       

        /// <summary>
        /// 根据当前值设置圆弧的EndAngle
        /// </summary>
        private void SetAngle()
        {
            if (this.Value < this.Minimum)
            {
                this.Angle = this.StartAngle;
                return;
            }

            if (this.Value > this.Maximum)
            {
                this.Angle = this.EndAngle;
                return;
            }

            var diff = this.Maximum - this.Minimum;
            var valueDiff = this.Value - this.Minimum;
            this.Angle = this.StartAngle + (this.EndAngle - this.StartAngle) / diff * valueDiff;
        }

        /// <summary>
        /// 角度值变化动画
        /// </summary>
        private void TransformAngle()
        {
            if (this.PART_IncreaseCircle != null)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation(this.OldAngle, this.Angle, this.TickDurtion);
                this.PART_IncreaseCircle.BeginAnimation(Arc.EndAngleProperty, doubleAnimation);
            }
        }
        #endregion
    }
}
