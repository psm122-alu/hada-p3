using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENProduct
    {
        // 1. Atributos privados (campos de respaldo)
        private string _code;
        private string _name;
        private int _amount;
        private float _price;
        private int _category;
        private DateTime _creationDate;

        // 2. Propiedades públicas
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public float Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        // 3. Constructores 
        public ENProduct()
        {
            // Constructor por defecto
        }

        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate)
        {
            this.Code = code;
            this.Name = name;
            this.Amount = amount;
            this.Price = price;
            this.Category = category;
            this.CreationDate = creationDate;
        }

        // 4. Métodos CRUD (Crear, Leer, Actualizar, Borrar) 
        public bool Create()
        {
            CADProduct cad = new CADProduct();
            return cad.Create(this); // Pasa este objeto (this) al CAD 
        }

        public bool Update()
        {
            CADProduct cad = new CADProduct();
            return cad.Update(this); 
        }

        public bool Delete()
        {
            CADProduct cad = new CADProduct();
            return cad.Delete(this); 
        }

        public bool Read()
        {
            CADProduct cad = new CADProduct();
            return cad.Read(this); 
        }

        public bool ReadFirst()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadFirst(this); 
        }

        public bool ReadNext()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadNext(this); 
        }

        public bool ReadPrev()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadPrev(this); 
        }
    }
}