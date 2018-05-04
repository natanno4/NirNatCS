using System;
using Model;

public class SettingsVM : ViewModal
{
    private Model model;
	public SettingsVM (ISettingsModel modell)
    {
        this.model = modell;
	}

}
