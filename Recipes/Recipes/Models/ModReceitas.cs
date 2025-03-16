using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recipes.Models
{
    [Table("receitas")]

    class ModReceitas
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public String titulo { get; set; }

        [NotNull]
        public String ingredientes { get; set; }

        [NotNull]
        public String link { get; set; }

        [NotNull]
        public Boolean favorito { get; set; }

        public ModReceitas()
        {
            this.id = 0;
            this.titulo = "";
            this.ingredientes = "";
            this.link = "";
            this.favorito = false;
        }
    }
}
