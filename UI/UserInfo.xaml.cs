using MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using MongoDB.Controllers;
using MongoDB.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        User CurrentUser;
        User DefUser;
        BL bl = new BL();
        public UserInfo(User us, User du)
        {
            InitializeComponent();
            CurrentUser = us;
            DefUser = du;
            UserName.Text = CurrentUser.FirstName;
            CountFollowers.Text = Convert.ToString(CurrentUser.FollowersId.Count);
            CountFollowings.Text = Convert.ToString(CurrentUser.FollowingsId.Count);

            foreach (var item in CurrentUser.Interests)
            {
                ShowInterests(item);
            }

            foreach (var item in CurrentUser.PostsId)
            {
                GeneratePost(bl.dt.GetEntityById<Post>("Posts",item));
            }

        }


        public void GeneratePost(Post ps)
        {
            Button likebtn = new Button();
            TextBlock PostText = new TextBlock();
            Canvas canvas = new Canvas();
            ScrollViewer scrollViewerPost = new ScrollViewer();
            TextBlock CreateBy = new TextBlock();
            TextBlock CountLike = new TextBlock();

            likebtn.Height = 30;
            likebtn.Width = 50;
            likebtn.Content = "Like";
            likebtn.Background = new SolidColorBrush(Colors.MistyRose);

            CreateBy.Text = bl.GetUserNameById(ps.UserId);
            canvas.Children.Add(CreateBy);
            Canvas.SetRight(CreateBy, 5);
            Canvas.SetBottom(CreateBy, 5);

            CountLike.Text = Convert.ToString(ps.UserLikesId.Count);
            canvas.Children.Add(CountLike);
            Canvas.SetRight(CountLike, 5);
            Canvas.SetBottom(CountLike, 20);

            canvas.Background = new SolidColorBrush(Colors.MistyRose);
            canvas.Height = 150;



            canvas.Children.Add(likebtn);
            Canvas.SetRight(likebtn, 5);
            Canvas.SetTop(likebtn, 40);

            PostText.Tag = string.Format($"_{ps.Id}");
            PostText.Width = 480;
            PostText.TextWrapping = TextWrapping.Wrap;
            PostText.Text = ps.Text;
            PostText.Foreground = new SolidColorBrush(Colors.Black);

            canvas.MouseDown += (sender, e) =>
            {
                Comments comform = new Comments(bl.dt.GetEntityById<Post>("Posts", ObjectId.Parse(Convert.ToString(PostText.Tag).Remove(0, 1))), DefUser);
                comform.Show();
            };

            likebtn.Click += (sender, e) =>
            {
                CountLike.Text = bl.like(PostText.Tag,DefUser);
            };

            scrollViewerPost.Height = 100;
            scrollViewerPost.Width = 500;
            scrollViewerPost.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewerPost.Content = PostText;

            Canvas.SetLeft(scrollViewerPost, 1);
            Canvas.SetTop(scrollViewerPost, 1);

            canvas.Children.Add(scrollViewerPost);
            AllPostStack.Children.Add(canvas);
        }


        public void ShowInterests(string text)
        {
            Canvas canvas = new Canvas();
            TextBlock Interest = new TextBlock();

            Interest.Width = 480;
            Interest.TextWrapping = TextWrapping.Wrap;
            Interest.Text = text;
            Interest.Foreground = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(Interest);
          
            canvas.Background = new SolidColorBrush(Colors.MistyRose);
            canvas.Height = 40;

            AllPeopleStack.Children.Add(canvas);
        }

    }
}
