using System;
using System.Windows;
using System.Windows.Controls;

namespace URLsTest.Client.Controls {
    /// <summary>
    /// Interaction logic for OkDialog.xaml
    /// </summary>
    public partial class OkDialog : UserControl {
        public OkDialog() {
            InitializeComponent();
        }

        public static DependencyProperty HeaderProperty = DependencyProperty.Register(
              name: "Header",
              propertyType: typeof(String),
              ownerType: typeof(OkDialog),
              typeMetadata: new FrameworkPropertyMetadata(
                  defaultValue: "Message Box",
                  flags: FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public static DependencyProperty MessageProperty = DependencyProperty.Register(
              name: "Message",
              propertyType: typeof(String),
              ownerType: typeof(OkDialog),
              typeMetadata: new FrameworkPropertyMetadata(
                  defaultValue: "Message",
                  flags: FrameworkPropertyMetadataOptions.AffectsRender)
            );

        public String Header {
            get { return (String)base.GetValue(HeaderProperty); }
            set { base.SetValue(HeaderProperty, value); }
        }

        public String Message {
            get { return (String)base.GetValue(MessageProperty); }
            set { base.SetValue(MessageProperty, value); }
        }
    }
}
