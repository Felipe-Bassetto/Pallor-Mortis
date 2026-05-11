using SQLite4Unity3d;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    private SQLiteConnection db;

    void Awake()
    {
        string dbPath = Path.Combine(Application.persistentDataPath, "savegame.db");
        if(!File.Exists(dbPath))
        {
            string origemDb = Application.dataPath + "/Projeto/Banco/savegame.db";
            string destinoDb = dbPath;
            File.Copy(origemDb, destinoDb);
        }

        db = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Banco criado/carregado em: " + dbPath);

    }

    // ---------------- SAVE ----------------
    public Save CarregarSave()
    {
        return db.Table<Save>().FirstOrDefault();
    }

    public void AtualizarSave(int id, float volMusic, bool fullScreen, int tempo)
    {
        db.Execute("UPDATE Save SET VolMusic = ?, FullScreen = ?, TempoDeJogo = ?   WHERE Id = ?", volMusic, fullScreen ? 1 : 0 , tempo, id);

    }


    // ---------------- INTERACOES ----------------
    public List<Memories> CarregarMemoria(int memoria)
    {
        return db.Table<Memories>().Where(m => m.Memoria == memoria).OrderBy(i => i.Ordem).ToList();
    }

    /*
    public void AtualizarSave(int id, float volMusic, bool fullScreen, int tempo)
    {
        db.Execute("UPDATE Save SET VolMusic = ?, FullScreen = ?, TempoDeJogo = ?   WHERE Id = ?", volMusic, fullScreen ? 1 : 0, tempo, id);

    }

    
    // ---------------- PROGRESSO ----------------
    public void SalvarProgresso(int nivel,  int id)
    {
        db.Execute("UPDATE Progresso SET Fase = ? WHERE Id = ?", nivel, id);

    }

    public Progresso CarregarProgresso()
    {
        return db.Table<Progresso>().FirstOrDefault();
    }

    /*public Colecao CarregarColecao(string desbloq, int id)
    {
        if (desbloq == null) return db.Table<Colecao>().FirstOrDefault();
        else return db.Table<Colecao>().Where(c => c.Coletado == desbloq && c.Id == id).FirstOrDefault();
    }

    // ---------------- COLE��O ----------------
    public void SalvarColecao(int id, bool coletado)
    {
        db.Execute("UPDATE Colecao SET Coletado = ? WHERE Id = ?", coletado ? 1: 0, id);
    }

    public Colecao CarregarColec(int idNum)
    {
        return db.Table<Colecao>().Where(c => c.Id == idNum).FirstOrDefault();
    }

    public List<Colecao> CarregarArtColec()
    {
        return db.Table<Colecao>().ToList();
    }*/

    void OnDestroy() //Passar para obj dontDestroy
    {
        db?.Close();
    }
}



// ---------------- MODELOS ----------------
public class Save
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public float VolMusic { get; set; }
    public int FullScreen { get; set; } // 0 ou 1
    public int ScreenWidth { get; set; }
    public int Screenheight { get; set; }
    public int CriancaPrinc { get; set; }
    public int TempoDeJogo { get; set; }
}

public class Memories
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Memoria { get; set; }
    public int Ordem { get; set; } 
    public string Fala { get; set; }
}

public class Recursos
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int IdSave { get; set; }
    public string Name { get; set; }
    public string Descricao { get; set; }
    public int QtdAtual { get; set; }
    
}

public class Construcoes
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int IdSave { get; set; }
    public string Name { get; set; }
    public string Descricao { get; set; }
    public int Requisito_1 { get; set; }
    public int Requisito_2 { get; set; }
    public int Requisito_3 { get; set; }
    public int Qtd_1 { get; set; }
    public int Qtd_2 { get; set; }
    public int Qtd_3 { get; set; }
}

public class Progresso
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int Fase { get; set; }
}

public class Marcos
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int IdSave { get; set; }
    public int IdCrianca { get; set; }
    public int Marco { get; set; }
    public int Brincadeira { get; set; }
    public string NomeBrincadeira { get; set; }
    public string MetodoVitoria { get; set; }
    public int Pontos { get; set; }
    public int Contador { get; set; }
}

public class Interacoes
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int IdCrianca { get; set; }
    public int NivelAmizade { get; set; }
    public int NumeroFala { get; set; }
    public string Fala { get; set; }
}

