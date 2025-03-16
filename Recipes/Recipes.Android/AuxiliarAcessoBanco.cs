using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipes.Droid
{
	class AuxiliarAcessoBanco
	{
		public static string ObterCaminhoLocalArquivo(string nomeArquivo)
		{
			string caminho = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

			return System.IO.Path.Combine(caminho, nomeArquivo);
		}
	}
}