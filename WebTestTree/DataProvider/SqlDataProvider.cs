using System;
using System.Data.SqlClient;
using System.Data;
using Tree;
using System.Collections.Generic;
namespace WebTestTree
{
    /// <summary>
    /// SQL-провайдер
    /// </summary>
    public class SqlDataProvider: AbstractDataProvider<PageNode>
    {
        /// <summary>
        /// Провайдер
        /// </summary>
        public static new AbstractDataProvider<PageNode> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SqlDataProvider();
                }
                return instance;
            }
        }

        /// <summary>
        /// Полуить ИД страницы по названию и уровню
        /// </summary>
        /// <param name="name">Имя страницы</param>
        /// <param name="level">Уровень страницы</param>
        /// <returns>ИД</returns>
        public override int GetPageId(string name, int level)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.GetPageId", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@name", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@page_level", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@name"].Value = name;
                    command.Parameters["@page_level"].Value = level;

                    command.Parameters["@page_id"].Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();

                    return Int32.Parse(command.Parameters["@page_id"].Value.ToString());
                }
            }
        }

        /// <summary>
        /// Установить значению узлу
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <param name="value">Значение узла</param>
        public override void SetValueNode(string id, PageNode value)
        {
            int page_id = int.Parse(id);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.SetPage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;

                    command.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
                    command.Parameters["@name"].Value = value.Name;
                    command.Parameters.Add(new SqlParameter("@title", SqlDbType.Text));
                    command.Parameters["@title"].Value = value.Title;
                    command.Parameters.Add(new SqlParameter("@content", SqlDbType.Text));
                    command.Parameters["@content"].Value = value.Content;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Получить значение узла
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Значение узла</returns>
        public override PageNode GetValueNode(string id)
        {
            int page_id = int.Parse(id);

            PageNode page = new PageNode();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.GetPage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["name"].ToString();
                            string title = reader["title"].ToString();
                            string content = reader["content"].ToString();
                            page = new PageNode()
                            {
                                Name = name,
                                Title = title,
                                Content = content

                            };
                        }
                    }
                    return page;
                }
            }
        }

        /// <summary>
        /// Установить родительский узел
        /// </summary>
        /// <param name="_id">ИД узла</param>
        /// <param name="_parent_id">ИД родительского узла</param>
        public override void SetParent(string _id, string _parent_id)
        {
            int page_id = int.Parse(_id);
            int parent_id = int.Parse(_parent_id);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.SetParent", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;

                    command.Parameters.Add(new SqlParameter("@parent_id", SqlDbType.Int));
                    command.Parameters["@parent_id"].Value = parent_id;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        /// <summary>
        /// Получить родительский узел
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Узел</returns>
        public override Tree.INode<PageNode> GetParent(string id)
        {
            int page_id = int.Parse(id);

            TreeNodeDataBase page = new TreeNodeDataBase();
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            using (SqlCommand command = new SqlCommand("dbo.GetPage", connection))
                            {
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                                command.Parameters["@page_id"].Value = page_id;

                                connection.Open();
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string in_id=reader["parent_id"].ToString();

                                        if (string.IsNullOrEmpty(in_id)) return null;
                                        page = new TreeNodeDataBase()
                                        {
                                            Name = in_id
                                        };
                                    }
                                }
                                return page;
                            }
                        }
        }

        /// <summary>
        /// Получить уровень узла
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Уровень</returns>
        public override uint GetLevel(string id)
        {
            int page_id = int.Parse(id);

            uint level = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.GetPage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string slevel= reader["level"].ToString();
                            level = uint.Parse(slevel);
                        }
                    }
                    return level;
                }
            }
        }

        /// <summary>
        /// Установить уровень узлу
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <param name="_level">Уровень узла</param>
        public override void SetLevel(string id, uint _level)
        {
            int page_id = int.Parse(id);
            int level = (int)_level;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.SetLevel", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;

                    command.Parameters.Add(new SqlParameter("@level", SqlDbType.Int));
                    command.Parameters["@level"].Value = level;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Коллекция дочерних узлов
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Коллекция узлов</returns>
        public override IList<INode<PageNode>> GetChildNodes(string id)
        {
            TreeNodeCollection<PageNode> list = new TreeNodeCollection<PageNode>();
            int page_id = int.Parse(id);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.GetChildPages", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string in_id = reader["id"].ToString();

                            TreeNodeDataBase page = new TreeNodeDataBase()
                            {
                                Name = in_id
                            }; 
                            list.Add(page);
                        }
                    }
                    return list;
                }
            }


            return list;
        }

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        /// <param name="name">ИД узла</param>
        /// <param name="newNode">Новый дочерний узел</param>
        public override void AddChildNode(string name, INode<PageNode> newNode)
        {
            int parent_id = int.Parse(name);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.AddPage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@name", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@title", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@content", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@parent_id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Direction = ParameterDirection.Output;
                    command.Parameters["@name"].Value = newNode.Value.Name;
                    command.Parameters["@title"].Value = newNode.Value.Title;
                    command.Parameters["@content"].Value = newNode.Value.Content;
                    command.Parameters["@parent_id"].Value = parent_id;

                    connection.Open();
                    command.ExecuteNonQuery();

                    //return Int32.Parse(command.Parameters["@page_id"].Value.ToString());
                }
            }
        }

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        /// <param name="name">ИД узла</param>
        /// <param name="newNode">Значение узла</param>
        /// <returns>Новый дочерний узел</returns>
        public override INode<PageNode> AddChildNode(string name, PageNode newNode)
        {
            int parent_id = int.Parse(name);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.AddPage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@name", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@title", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@content", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@parent_id", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Direction = ParameterDirection.Output;
                    command.Parameters["@name"].Value = newNode.Name;
                    command.Parameters["@title"].Value = newNode.Title;
                    command.Parameters["@content"].Value = newNode.Content;
                    command.Parameters["@parent_id"].Value = parent_id;
                    command.Parameters["@page_id"].Direction = ParameterDirection.Output;
         

                   
                    connection.Open();
                    command.ExecuteNonQuery(); 
                    string in_id = command.Parameters["@page_id"].Value.ToString();
                    TreeNodeDataBase page = new TreeNodeDataBase()
                    {
                        Name = in_id
                    };
                    return page;
                    //return Int32.Parse(command.Parameters["@page_id"].Value.ToString());
                }
            }
        }

        /// <summary>
        /// Удалить узел
        /// </summary>
        /// <param name="name">ИД узла</param>
        public override void RemoveNode(string name)
        {
            int page_id = int.Parse(name);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.RemovePage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Изменить значение узла
        /// </summary>
        /// <param name="name">ИД узла</param>
        /// <param name="node">Новые значения узла</param>
        public  void EditNode(string name,PageNode node)
        {
            int page_id = int.Parse(name);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.EditPage", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@title", SqlDbType.Text));
                    command.Parameters.Add(new SqlParameter("@content", SqlDbType.Text));
                    command.Parameters["@title"].Value = node.Title;
                    command.Parameters["@content"].Value = node.Content;
                    command.Parameters.Add(new SqlParameter("@page_id", SqlDbType.Int));
                    command.Parameters["@page_id"].Value = page_id;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

