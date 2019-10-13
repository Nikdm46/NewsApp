using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace NewsAppNative.Core.Models
{
    public class NewsModel : MvxViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        private string _buttonTitle = "Показать еще...";
        public string ButtonTitle
        {
            get => _buttonTitle;
            set
            {
                _buttonTitle = value;
                RaisePropertyChanged(() => ButtonTitle);
            }
        }
        public DateTime CreatedAt { get; set; }

        private bool _isMuted;
        public bool IsMuted
        {
            get => _isMuted;
            set
            {
                _isMuted = value;
                RaisePropertyChanged(() => IsMuted);
            }
        }

        private bool _isInFavorite;
        public bool IsInFavorite
        {
            get => _isInFavorite;
            set
            {
                _isInFavorite = value;
                if(_isInFavorite)
                {
                    InFavoriteImageSource = new Uri("res:baseline_favorite_white_24.png");
                }
                else
                {
                    InFavoriteImageSource = new Uri("res:baseline_favorite_border_white_24.png");
                }
                RaisePropertyChanged(() => IsInFavorite);
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                if(_isExpanded)
                {
                    ButtonTitle = "Скрыть";
                }
                else
                {
                    ButtonTitle = "Показать еще...";
                }
                RaisePropertyChanged(() => IsExpanded);
            }
        }

        private Uri _inFavoriteImageSource = new Uri("res:baseline_favorite_border_white_24.png");
        public Uri InFavoriteImageSource
        {
            get => _inFavoriteImageSource;
            set
            {
                _inFavoriteImageSource = value;
                RaisePropertyChanged(() => InFavoriteImageSource);
            }
        }

        public NewsModel()
        {
        }

        public MvxCommand ExpandTextCommand => new MvxCommand(ExpandText);

        private void ExpandText()
        {
            if (IsExpanded)
            {
                IsExpanded = false;
            }
            else
            {
                IsExpanded = true;
            }
        }
    }
}
