using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermTracker.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateTerm : ContentPage
	{
		int id = ViewTerms.termId;
		public UpdateTerm()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			nameEntry.Text = DB.GetTermName(id);
			startDatePicker.Date = DB.GetTermStart(id);
			endDatePicker.Date = DB.GetTermEnd(id);
		}

		private void SaveTerm_Clicked(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(nameEntry.Text))
			{
				DisplayAlert("Missing Term Name", "Please enter a name", "OK");
				return;
			}
			if (startDatePicker.Date > endDatePicker.Date)
			{
				DisplayAlert("Invalid Date", "The end of the term cannot be before the start", "Ok");
				return;
			}
			else
			{
				DB.AddTerm(nameEntry.Text, startDatePicker.Date, endDatePicker.Date);
				Shell.Current.GoToAsync("HomePage");
			}
		}

		private void cancelButton_Clicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}