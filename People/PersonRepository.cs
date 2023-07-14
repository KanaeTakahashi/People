using People.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class PersonRepository
    {
        public string StatusMessage { get; set; }

        private string _dbPath;
        //private SQLiteConnection conn;
        private SQLiteAsyncConnection conn;

        //private void Init()
        //{
        //    if (conn != null) return;

        //    conn = new SQLiteConnection(_dbPath);
        //    conn.CreateTable<Person>();
        //}

        /// <summary>
        /// 非同期のInit
        /// </summary>
        /// <returns></returns>
        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<Person>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;                        
        }

        //public void AddNewPerson(string name)
        //{            
        //    int result = 0;
        //    try
        //    {
        //        Init();

        //        // basic validation to ensure a name was entered
        //        if (string.IsNullOrEmpty(name))
        //            throw new Exception("Valid name required");

        //        result = conn.Insert(new Person { Name = name });

        //        StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        //    }

        //}

        /// <summary>
        /// 非同期の新規追加メソッド
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                // Call Init()
                await Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new Person { Name = name });

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }


        //public List<Person> GetAllPeople()
        //{
        //    try
        //    {
        //        Init();
        //        return conn.Table<Person>().ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        //    }

        //    return new List<Person>();
        //}

        /// <summary>
        /// 非同期で全データを取得
        /// </summary>
        /// <returns></returns>
        public async Task<List<Person>> GetAllPeople()
        {
            try
            {
                await Init();
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}
