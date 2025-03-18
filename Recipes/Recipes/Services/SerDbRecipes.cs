using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

using Recipes.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Recipes.Services
{
    class SerDbRecipes
    {
        SQLiteConnection conn;

        public string MensagemStatus { get; set; }

        public SerDbRecipes(string dbCaminho)
        {
            if (dbCaminho == "")
                dbCaminho = App.DbCaminho;

            conn = new SQLiteConnection(dbCaminho);
            conn.CreateTable<ModReceitas>();
        }

        public void Inserir(ModReceitas receita)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(receita.titulo))
                    throw new Exception("'Títutlo' não informado!");

                if (string.IsNullOrWhiteSpace(receita.ingredientes))
                    receita.ingredientes = "";

                if (string.IsNullOrWhiteSpace(receita.link))
                    receita.link = "";

                int res = conn.Insert(receita);

                this.MensagemStatus = res != 0 ? $"Receita salva:\n\n[{receita.titulo}]" : "Ocorreu um erro!\n\nTente novamente!";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir receita: {ex.Message}", ex);
            }
        }

        public List<ModReceitas> Listar()
        {
            try
            {
                List<ModReceitas> li = conn.Table<ModReceitas>().ToList();

                this.MensagemStatus = "Listagem das anotações!";

                return li;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar receita: {ex.Message}", ex);
            }
        }

        public async Task<bool> Alterar(ModReceitas receita, Page page)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(receita.titulo))
                    throw new Exception("'Títutlo' não informado!");

                if (string.IsNullOrWhiteSpace(receita.ingredientes))
                {
                    var result = await page.DisplayAlert(
                        "Confirmar Alteração",
                        "O campo de ingredientes está vazio. Você deseja continuar com a alteração?",
                        "Sim", "Não");

                    if (!result) return false; 
                }

                if (string.IsNullOrWhiteSpace(receita.link))
                {
                    var result = await page.DisplayAlert(
                        "Confirmar Alteração",
                        "O campo de link está vazio. Você deseja continuar com a alteração?",
                        "Sim", "Não");

                    if (!result) return false; 
                }

                int res = conn.Update(receita);

                this.MensagemStatus = res != 0 ? $"Receita alterada:\n\n[{receita.titulo}]" : "Ocorreu um erro!\n\nTente novamente!";

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar receita: {ex.Message}", ex);
            }
        }

        public void Excluir(int id)
        {
            try
            {
                int res = conn.Table<ModReceitas>().Delete(r => r.id == id);

                MensagemStatus = res == 1 ? $"{res} registro excluído!" : $"{res} registros excluídos!\n\nTente novamente!";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir receita: {ex.Message}", ex);
            }
        }

        public List<ModReceitas> Localizar(string titulo)
        {
            try
            {
                TableQuery<ModReceitas> receita = conn.Table<ModReceitas>();
                TableQuery<ModReceitas> res = from p in receita 
                                              where p.titulo.ToLower().Contains(titulo.ToLower()) 
                                              select p;
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao localizar receita: {ex.Message}", ex);
            }
        }

        public List<ModReceitas> Localizar(string titulo, Boolean fav)
        {
            try
            {
                TableQuery<ModReceitas> receita = conn.Table<ModReceitas>();
                TableQuery<ModReceitas> res = from p in receita
                                              where p.titulo.ToLower().Contains(titulo.ToLower()) && p.favorito == fav
                                              select p;
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao localizar receita: {ex.Message}", ex);
            }
        }
    }
}
