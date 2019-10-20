using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using NewsAppNative.Core.Models;
using NewsAppNative.iOS.Converters;
using NewsAppNative.iOS.Helpers;
using UIKit;

namespace NewsAppNative.iOS.Views.Cells
{
    [Register("NewsCell")]
    public class NewsCell: MvxTableViewCell
    {
        private UILabel _title;
        private UILabel _content;
        private UILabel _contentSmall;
        private UILabel _createdAt;
        private UIButton _button;
        private UIStackView _stack;
        public NewsCell()
        {
            CreateLayout();
            InitializeBindings();
        }

        public NewsCell(IntPtr handle) : base(handle)
        {
            CreateLayout();
            InitializeBindings();
        }

        private void CreateLayout()
        {
            //ContentView.Transform = CGAffineTransform.MakeScale(1, -1);
            _stack = new UIStackView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 5,
                Axis = UILayoutConstraintAxis.Vertical,
            };
            _title = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Bold),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Lines = 1,
            };
            _content = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Lines = 1,
            };
            _contentSmall = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Lines = 5
            };
            _createdAt = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Font = UIFont.SystemFontOfSize(11f, UIFontWeight.Regular),
                TextColor = UIColor.Gray,
                TextAlignment = UITextAlignment.Left,
                BackgroundColor = UIColor.Clear,
                Lines = 1
            };
            _button = new UIButton()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };

            //_button.SetTitle("Показать еще...", UIControlState.Normal);
            _button.SetTitleColor(UIColor.Black, UIControlState.Normal);

            ContentView.AddSubviews(_stack);

            _stack.AddArrangedSubview(_title);
            _stack.AddArrangedSubview(_createdAt);
            _stack.AddArrangedSubview(_content);
            _stack.AddArrangedSubview(_contentSmall);
            _stack.AddArrangedSubview(_button);

            _stack.HeightEqualTo(ContentView, ContentView);
            _stack.WidthEqualTo(ContentView, ContentView);
            _stack.LeftEqualTo(ContentView, ContentView);
            _stack.RightEqualTo(ContentView, ContentView);
            _stack.TopEqualTo(ContentView, ContentView);
            _stack.BottomEqualTo(ContentView, ContentView);

            _title.WidthEqualTo(_stack, _stack, margin: -20);
            _title.LeftEqualTo(_stack, _stack, margin: 10);
            _title.RightEqualTo(_stack, _stack, margin: -10);

            _createdAt.WidthEqualTo(_stack, _stack, margin: -20);
            _createdAt.LeftEqualTo(_stack, _stack, margin: 10);
            _createdAt.RightEqualTo(_stack, _stack, margin: -10);

            _contentSmall.WidthEqualTo(_stack, _stack, margin: -20);
            _contentSmall.LeftEqualTo(_stack, _stack, margin: 10);
            _contentSmall.RightEqualTo(_stack, _stack, margin: -10);

            _content.WidthEqualTo(_stack, _stack, margin: -20);
            _content.LeftEqualTo(_stack, _stack, margin: 10);
            _content.RightEqualTo(_stack, _stack, margin: -10);
        }

        private void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<NewsCell, NewsModel>();
                set.Bind(_title).To(vm => vm.Title);
                set.Bind(_content).To(vm => vm.Content);
                set.Bind(_content).For(i => i.Hidden).To(vm => vm.IsExpanded).WithConversion<InvertBoolConverter>();
                set.Bind(_contentSmall).To(vm => vm.Content);
                set.Bind(_contentSmall).For(i => i.Hidden).To(vm => vm.IsExpanded);
                set.Bind(_createdAt).To(vm => vm.CreatedAt).WithConversion<DateTimeToStringValueConverter>();                
                set.Bind(_button).For("Title").To(vm => vm.ButtonTitle);
                set.Bind(_button).To(vm => vm.ExpandTextCommand);
                set.Apply();
            });
        }
    }
}
