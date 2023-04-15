using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Models
{
	public class Term
	{
		[PrimaryKey, AutoIncrement]
		public int TermId { get; set; }
		public string TermName { get; set; }
		public string TermNameOutput => $"{TermName.ToUpper()}";
		public DateTime TermStart { get; set; }
		public DateTime TermEnd { get; set; }
		public string TermDate => $"Start {TermStart.ToShortDateString()} {Environment.NewLine} End {TermEnd.ToShortDateString()}";

		public List<Term> terms = new List<Term>();
	}
}
