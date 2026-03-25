using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;

namespace proWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CADCategory cad = new CADCategory();
                List<ENCategory> lista = cad.readAll();
                ddlCategory.DataSource = lista;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();
            }

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ""; // Limpiar mensajes anteriores
            lblMessage.ForeColor = System.Drawing.Color.Red; 

            try
            {
                // Validaciones básicas de los campos de texto
                if (string.IsNullOrWhiteSpace(txtCode.Text) || txtCode.Text.Length > 16)
                {
                    lblMessage.Text = "Error: El código es obligatorio y debe tener máximo 16 caracteres.";
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtName.Text) || txtName.Text.Length > 32)
                {
                    lblMessage.Text = "Error: El nombre es obligatorio y debe tener máximo 32 caracteres.";
                    return;
                }

                int amount;
                if (!int.TryParse(txtAmount.Text, out amount) || amount < 0 || amount > 9999)
                {
                    lblMessage.Text = "Error: La cantidad debe ser un número entero entre 0 y 9999.";
                    return;
                }

                float price;
                if (!float.TryParse(txtPrice.Text, out price) || price < 0 || price > 9999.99f)
                {
                    lblMessage.Text = "Error: El precio debe ser un número real positivo (ej. 15,50).";
                    return;
                }

                DateTime creationDate;
                if (!DateTime.TryParse(txtCreationDate.Text, out creationDate))
                {
                    lblMessage.Text = "Error: Formato de fecha incorrecto. Usa dd/mm/aaaa hh:mm:ss.";
                    return;
                }

                // Comprobar si el producto ya existe en la base de datos
                ENProduct productoComprobacion = new ENProduct();
                productoComprobacion.Code = txtCode.Text;

                if (productoComprobacion.Read())
                {
                    lblMessage.Text = "Error: Ya existe un producto con el código " + txtCode.Text;
                    return;
                }

                // Crear el nuevo producto
                ENProduct nuevoProducto = new ENProduct(
                    txtCode.Text,
                    txtName.Text,
                    amount,
                    price,
                    int.Parse(ddlCategory.SelectedValue),
                    creationDate
                );

                if (nuevoProducto.Create())
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Producto creado correctamente.";
                }
                else
                {
                    lblMessage.Text = "Error al guardar el producto en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Se ha producido un error inesperado: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e) { }
        protected void btnDelete_Click(object sender, EventArgs e) { }
        protected void btnRead_Click(object sender, EventArgs e) { }
        protected void btnReadFirst_Click(object sender, EventArgs e) { }
        protected void btnReadPrev_Click(object sender, EventArgs e) { }
        protected void btnReadNext_Click(object sender, EventArgs e) { }
    }
}