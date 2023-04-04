using mtg_lib.Library.Models;

namespace mtg_app.Models;
using System;
using System.Collections.Generic;

public class HomeViewModel
{
    public long Pages { get; set; }
    public int Index { get; set; }
    public String Name { get; set; }
    
    public String Direction { get; set; }
    public String Manacost { get; set; }
    public List<CardViewModel> Cards { get; set; }
}