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

        protected void ButtonCreate_Click(object sender, EventArgs e)
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


        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Red;

            try
            {
                
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
                    lblMessage.Text = "Error: El precio debe ser un número real positivo.";
                    return;
                }

                DateTime creationDate;
                if (!DateTime.TryParse(txtCreationDate.Text, out creationDate))
                {
                    lblMessage.Text = "Error: Formato de fecha incorrecto. Usa dd/mm/aaaa hh:mm:ss.";
                    return;
                }

                // Comprobar que el producto si exista antes de actualizarlo
                ENProduct productoComprobacion = new ENProduct();
                productoComprobacion.Code = txtCode.Text;

                if (!productoComprobacion.Read())
                {
                    lblMessage.Text = "Error: No existe ningún producto con el código " + txtCode.Text + " para actualizar.";
                    return;
                }

                // Actualizar el producto con los nuevos datos
                ENProduct productoActualizar = new ENProduct(
                    txtCode.Text,
                    txtName.Text,
                    amount,
                    price,
                    int.Parse(ddlCategory.SelectedValue),
                    creationDate
                );

                if (productoActualizar.Update())
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Producto actualizado correctamente.";
                }
                else
                {
                    lblMessage.Text = "Error al actualizar el producto en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Se ha producido un error inesperado: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Red;

            // Comprobamos que haya un código escrito
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                lblMessage.Text = "Error: Introduce el código del producto que quieres borrar.";
                return;
            }

            try
            {
                ENProduct productoBorrar = new ENProduct();
                productoBorrar.Code = txtCode.Text;

                // Intentamos borrarlo
                if (productoBorrar.Delete())
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Producto borrado correctamente.";

                    // Limpiamos los campos visualmente
                    txtCode.Text = "";
                    txtName.Text = "";
                    txtAmount.Text = "";
                    txtPrice.Text = "";
                    txtCreationDate.Text = "";
                }
                else
                {
                    lblMessage.Text = "Error: No se ha podido borrar. Comprueba que el código " + txtCode.Text + " exista.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Se ha producido un error inesperado: " + ex.Message;
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
            }
        }
        protected void ButtonRead_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Red;

            // Comprobamos que al menos haya escrito un código
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                lblMessage.Text = "Error: Introduce un código para buscar el producto.";
                return;
            }

            ENProduct producto = new ENProduct();
            producto.Code = txtCode.Text;

            
            if (producto.Read())
            {
                
                txtName.Text = producto.Name;
                txtAmount.Text = producto.Amount.ToString();
                txtPrice.Text = producto.Price.ToString();
                ddlCategory.SelectedValue = producto.Category.ToString();
                txtCreationDate.Text = producto.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Producto leído correctamente.";
            }
            else
            {
                lblMessage.Text = "Error: No se ha encontrado ningún producto con el código " + txtCode.Text;
            }
        }
        protected void ButtonReadFirst_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Red;

            try
            {
                ENProduct producto = new ENProduct();

                if (producto.ReadFirst())
                {
                    txtCode.Text = producto.Code;
                    txtName.Text = producto.Name;
                    txtAmount.Text = producto.Amount.ToString();
                    txtPrice.Text = producto.Price.ToString();
                    ddlCategory.SelectedValue = producto.Category.ToString();
                    txtCreationDate.Text = producto.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Primer producto leído correctamente";
                }
                else
                {
                    lblMessage.Text = "La base de datos de productos está vacía";               
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }

        }
        protected void ButtonReadPrev_Click(object sender, EventArgs e) 
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Red;

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                lblMessage.Text = "Error: introducir un código para buscar el anterior";
                return;
            }
                
            try
            {
                ENProduct producto = new ENProduct();
                producto.Code = txtCode.Text;

                if (producto.ReadPrev())
                {
                    txtCode.Text = producto.Code;
                    txtName.Text = producto.Name;
                    txtAmount.Text = producto.Amount.ToString();
                    txtPrice.Text = producto.Price.ToString();
                    ddlCategory.SelectedValue = producto.Category.ToString();
                    txtCreationDate.Text = producto.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Producto anterior leído correctamente";
                }
                else
                {
                    lblMessage.Text = "No hay más productos antes del código introducido: " + txtCode.Text;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
        protected void ButtonReadNext_Click(object sender, EventArgs e) 
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = System.Drawing.Color.Red;

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                lblMessage.Text = "Error: introducir un código para buscar el siguiente";
                return;
            }

            try
            {
                ENProduct producto = new ENProduct();
                producto.Code = txtCode.Text;

                if (producto.ReadNext())
                {
                    txtCode.Text = producto.Code;
                    txtName.Text = producto.Name;
                    txtAmount.Text = producto.Amount.ToString();
                    txtPrice.Text = producto.Price.ToString();
                    ddlCategory.SelectedValue = producto.Category.ToString();
                    txtCreationDate.Text = producto.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Producto siguiente leído correctamente";
                }
                else
                {
                    lblMessage.Text = "No hay más productos después del código introducido: " + txtCode.Text;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}