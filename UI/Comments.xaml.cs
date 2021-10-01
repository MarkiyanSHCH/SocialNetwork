using MongoDB.Bson;
using MongoDB.Controllers;
using MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI
{
    /// <summary>
    /// Interaction logic for Comments.xaml
    /// </summary>
    public partial class Comments : Window
    {
        BL bl = new BL();
        User currentUser;
        Post currentPost;
        public Comments(Post ps,User us)
        {
            InitializeComponent();
            currentPost = ps;
            currentUser = us;
            foreach (var item in currentPost.CommentsId)
            {
                GenerateComments(bl.dt.GetEntityById<Comment>("Comments",item));
            }
        }

        public void GenerateComments(Comment cm)
        {
            
            TextBlock PostText = new TextBlock();
            Canvas canvas = new Canvas();
            ScrollViewer scrollViewerPost = new ScrollViewer();
            TextBlock CreateBy = new TextBlock();

            CreateBy.Text = bl.GetUserNameById(cm.UserId);
            canvas.Children.Add(CreateBy);
            Canvas.SetRight(CreateBy, 5);
            Canvas.SetBottom(CreateBy, 5);

            canvas.Background = new SolidColorBrush(Colors.MistyRose);
            canvas.Height = 100;

           
            PostText.Tag = string.Format($"_{cm.Id}");
            PostText.Width = 480;
            PostText.TextWrapping = TextWrapping.Wrap;
            PostText.Text = cm.Text;
            PostText.Foreground = new SolidColorBrush(Colors.Black);

            scrollViewerPost.Height = 100;
            scrollViewerPost.Width = 500;
            scrollViewerPost.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewerPost.Content = PostText;

            Canvas.SetLeft(scrollViewerPost, 1);
            Canvas.SetTop(scrollViewerPost, 1);

            canvas.Children.Add(scrollViewerPost);
            AllCommentStack.Children.Add(canvas);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Comment cm = new Comment()
            {
                Text = CommentText.Text,
                insertTime = DateTime.Now,
                UserId = currentUser.Id,
            };

            CommentText.Text = "";
            bl.AddComment(currentPost.Id, cm);
            GenerateComments(cm);
        }
    }
}
