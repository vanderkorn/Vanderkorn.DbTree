using System;
using System.Collections.Generic;
using System.Text;

namespace Tree
{
    /// <summary>
    /// Абстрактный провайдер
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractDataProvider<T>
    {
        /// <summary>
        /// Строка соединения
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Констуктор
        /// </summary>
        protected AbstractDataProvider() { }

        /// <summary>
        /// Провайдер
        /// </summary>
        protected static AbstractDataProvider<T> instance;

        /// <summary>
        /// Провайдер
        /// </summary>
        public static AbstractDataProvider<T> Instance;


        /// <summary>
        /// Полуить ИД страницы по названию и уровню
        /// </summary>
        /// <param name="name">Имя страницы</param>
        /// <param name="level">Уровень страницы</param>
        /// <returns>ИД</returns>
        public abstract int GetPageId(string name, int level);

        /// <summary>
        /// Установить значению узлу
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <param name="value">Значение узла</param>
        public abstract void SetValueNode(string id, T value);

        /// <summary>
        /// Получить значение узла
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Значение узла</returns>
        public abstract T GetValueNode(string id);

        /// <summary>
        /// Установить родительский узел
        /// </summary>
        /// <param name="_id">ИД узла</param>
        /// <param name="_parent_id">ИД родительского узла</param>
        public abstract void SetParent(string _id, string _parent_id);

        /// <summary>
        /// Получить родительский узел
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Узел</returns>
        public abstract Tree.INode<T> GetParent(string id);

        /// <summary>
        /// Получить уровень узла
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Уровень</returns>
        public abstract  uint GetLevel(string id);

        /// <summary>
        /// Установить уровень узлу
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <param name="_level">Уровень узла</param>
        public abstract void SetLevel(string id, uint _level);

        /// <summary>
        /// Коллекция дочерних узлов
        /// </summary>
        /// <param name="id">ИД узла</param>
        /// <returns>Коллекция узлов</returns>
        public abstract IList<INode<T>> GetChildNodes(string id);

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        /// <param name="name">ИД узла</param>
        /// <param name="newNode">Новый дочерний узел</param>
        public abstract void AddChildNode(string name, INode<T> newNode);

        /// <summary>
        /// Добавить дочерний узел
        /// </summary>
        /// <param name="name">ИД узла</param>
        /// <param name="newNode">Значение узла</param>
        /// <returns>Новый дочерний узел</returns>
        public abstract INode<T> AddChildNode(string name, T newNode);

        /// <summary>
        /// Удалить узел
        /// </summary>
        /// <param name="name">ИД узла</param>
        public abstract void RemoveNode(string name);
    }
}
