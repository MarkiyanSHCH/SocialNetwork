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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public User DefUser;
        public BL bl = new BL();
        public MainWindow(User us)
        {
            InitializeComponent();
            DefUser = us;
            UserName.Text = DefUser.FirstName;
            CountFollowers.Text = Convert.ToString(DefUser.FollowersId.Count);
            CountFollowings.Text = Convert.ToString(DefUser.FollowingsId.Count);
            printPosts();
            foreach (var item in bl.users)
            {
                if (item.Id == DefUser.Id)
                    continue;
                ShowPeople(item);
            }

        }

        public void printPosts()
        {
            AllPostStack.Children.Clear();
            DataController dt = new DataController("MongoSocialNetwork");
            var posts = dt.ReadEntity<Post>("Posts");
            posts = posts.OrderByDescending(p => p.insertTime).ToList();
            foreach (var item in posts)
            {
                GeneratePost(item);
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
                Comments comform = new Comments(bl.dt.GetEntityById<Post>("Posts", ObjectId.Parse(Convert.ToString(PostText.Tag).Remove(0,1))), DefUser);
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

        public void ShowPeople(User us)
        {
            Button AddOrDelete = new Button();
            Canvas canvas = new Canvas();
            TextBlock PeopleName = new TextBlock();

            AddOrDelete.Height = 30;
            AddOrDelete.Width = 30;
            AddOrDelete.Content = "+";
            AddOrDelete.Background = new SolidColorBrush(Colors.MistyRose);
            Canvas.SetRight(AddOrDelete, 5);
            Canvas.SetTop(AddOrDelete, 5);
            canvas.Children.Add(AddOrDelete);


            PeopleName.Tag = string.Format($"_{us.Id}");
            PeopleName.Width = 480;
            PeopleName.TextWrapping = TextWrapping.Wrap;
            PeopleName.Text = us.FirstName + " " + us.SecondName;
            PeopleName.Foreground = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(PeopleName);

            canvas.MouseDown += (sender, e) =>
            {
                UserInfo ui = new UserInfo(bl.dt.GetEntityById<User>("Users", ObjectId.Parse(Convert.ToString(PeopleName.Tag).Remove(0, 1))),DefUser);
                ui.Show();
            };

            AddOrDelete.Click += (sender, e) =>
            {
                CountFollowers.Text = bl.Followers(Convert.ToString(PeopleName.Tag).Remove(0, 1),DefUser);
                CountFollowings.Text = bl.Followings(Convert.ToString(PeopleName.Tag).Remove(0, 1), DefUser);
            };

            canvas.Background = new SolidColorBrush(Colors.MistyRose);
            canvas.Height = 40;

            AllPeopleStack.Children.Add(canvas);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 createpost = new Window1(this);
            createpost.Show();

        }

    }
}
