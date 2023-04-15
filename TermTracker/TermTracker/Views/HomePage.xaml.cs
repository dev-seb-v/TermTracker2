using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Models;
using TermTracker.Services;
using TermTracker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public static int term_id;
		public static int courseId;
		public static Term t;
		public HomePage()
		{
			InitializeComponent();
			using (SQLiteConnection con = new SQLiteConnection(DB.databasePath))
			{
				con.CreateTable<Term>();
				List<Term> terms = con.Table<Term>().ToList();
				termPicker.ItemsSource = terms;
			}
			if (termPicker.SelectedItem == null)
			{
				GoToAddCourse.IsEnabled = false;
			}
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			termPicker.SelectedIndex = -1;
		}
		private void GoToAddCourse_Clicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync("AddCourse");
		}

		private void HomeButton_Clicked(object sender, EventArgs e)
		{
			(Application.Current).MainPage = new AppShell();
		}

		private void termPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			t = (Term)termPicker.SelectedItem;
			term_id = t.TermId;
			GoToAddCourse.IsEnabled = true;

			CourseView.ItemsSource = DB.ReadCourses(term_id);
		}

		private void CourseView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			courseId = (e.CurrentSelection.FirstOrDefault() as Course).CourseId;
			//Shell.Current.GoToAsync("ViewCourse");
			Navigation.PushAsync(new CourseDetailPage());
		}
	}
}