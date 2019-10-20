using UIKit;

namespace NewsAppNative.iOS.Helpers
{
    public static class ConstraintsHelper
    {
        public static void WidthEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Width, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Width, multiplier, margin));
        }

        public static void WidthEqualHeightTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1,
            float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Width, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Height, multiplier, margin));
        }

        public static void HeightEqualWidthTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1,
            float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Height, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Width, multiplier, margin));
        }

        public static void WidthGreaterThanOrEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1,
            float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Width, NSLayoutRelation.GreaterThanOrEqual,
                view2, NSLayoutAttribute.Width, multiplier, margin));
        }

        public static void WidthLessThanOrEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1,
            float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Width, NSLayoutRelation.LessThanOrEqual,
                view2, NSLayoutAttribute.Width, multiplier, margin));
        }

        public static void HeightEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Height, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Height, multiplier, margin));
        }

        public static void HeightGreaterThanOrEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1,
            float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Height, NSLayoutRelation.GreaterThanOrEqual,
                view2, NSLayoutAttribute.Height, multiplier, margin));
        }

        public static void HeightLessThanOrEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1,
            float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Height, NSLayoutRelation.LessThanOrEqual,
                view2, NSLayoutAttribute.Height, multiplier, margin));
        }

        public static void LeftEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Left, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Left, multiplier, margin));
        }

        public static void RightEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Right, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Right, multiplier, margin));
        }

        public static void TopEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Top, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Top, multiplier, margin));
        }

        public static void BottomEqualTo(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Bottom, multiplier, margin));
        }

        public static void ToLeftOf(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Right, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Left, multiplier, margin));
        }

        public static void ToRightOf(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Left, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Right, multiplier, margin));
        }

        public static void Below(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Top, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Bottom, multiplier, margin));
        }

        public static void Above(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.Top, multiplier, margin));
        }

        public static void CenterYOf(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.CenterY, multiplier, margin));
        }

        public static void CenterXOf(this UIView view1, UIView parent, UIView view2, float multiplier = 1, float margin = 0)
        {
            parent.AddConstraint(NSLayoutConstraint.Create(view1, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, view2,
                NSLayoutAttribute.CenterX, multiplier, margin));
        }
    }
}
