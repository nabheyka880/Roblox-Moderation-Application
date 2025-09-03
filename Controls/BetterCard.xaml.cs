using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Roblox_Moderation_Application_WPF.Controls
{
    /// <summary>
    /// Interaction logic for BetterCard.xaml
    /// </summary>
    public partial class BetterCard : UserControl
    {
        public BetterCard()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(BetterCard), new PropertyMetadata(""));

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(BetterCard), new PropertyMetadata(""));

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty CardContentProperty = DependencyProperty.Register(nameof(CardContent), typeof(object), typeof(BetterCard), new PropertyMetadata(null));

        public object CardContent
        {
            get => GetValue(CardContentProperty);
            set => SetValue(CardContentProperty, value);
        }
    }
}
