using MongoDB.Bson;
using MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        MainWindow mw = null;
        public Window1(Window tmp)
        {
            mw = tmp as MainWindow;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Post ps = new Post()
            {
                Text = PostText.Text,
                insertTime = DateTime.Now,
                UserId = mw.DefUser.Id,
                CommentsId = new List<ObjectId>(),
                UserLikesId = new List<ObjectId>()
            };

            mw.bl.AddPost(mw.DefUser.Id, ps);

            this.mw.GeneratePost(ps);
            mw.printPosts();
            Close();
        }
    }
}
