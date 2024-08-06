using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestProam
{
    public partial class Results : System.Web.UI.Page
    {
        private string connectionString = "Server=DESKTOP-QCFUCML;Database=TestProamDb;Trusted_Connection=True;";
        private DBHelper dbHelper;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbHelper = new DBHelper(connectionString);
        }

        protected void btnProyectoProductos_Click(object sender, EventArgs e)
        {
            string query = @"
                SELECT p.NOMBRE AS Proyecto, pr.DESCRIPCION AS Producto
                FROM PROYECTO p
                JOIN PRODUCTO_PROYECTO pp ON p.PROYECTO = pp.PROYECTO
                JOIN PRODUCTO pr ON pp.PRODUCTO = pr.PRODUCTO 
                WHERE p.PROYECTO = 1;";
            DataSet ds = dbHelper.ExecuteSelectCommand(query);
            gvResultados.DataSource = ds;
            gvResultados.DataBind();
        }

        protected void btnMensaje_Click(object sender, EventArgs e)
        {
            string query = @"
                SELECT M.COD_MENSAJE,FM.ASUNTO As Asunto, P.NOMBRE AS Proyecto, PR.DESCRIPCION AS Producto
                FROM MENSAJE M
				JOIN FORMATO_MENSAJE FM ON M.COD_FORMATO = FM.COD_FORMATO
                JOIN PROYECTO P ON M.PROYECTO = P.PROYECTO
                JOIN PRODUCTO PR ON M.PRODUCTO = PR.PRODUCTO";
            DataSet ds = dbHelper.ExecuteSelectCommand(query);
            gvResultados.DataSource = ds;
            gvResultados.DataBind();
        }

        protected void btnMensajeTodos_Click(object sender, EventArgs e)
        {
            string query = @"WITH TotalProductosPorProyecto AS (
                SELECT PROYECTO, COUNT(DISTINCT PRODUCTO) AS TotalProductos
                FROM PRODUCTO_PROYECTO
                GROUP BY PROYECTO
            ),
            ProductosMensaje AS (
                SELECT 
                    M.PROYECTO,
                    M.COD_FORMATO,
                    COUNT(DISTINCT M.PRODUCTO) AS ProductosMensaje
                FROM MENSAJE M
                GROUP BY M.PROYECTO, M.COD_FORMATO
            ),
            MensajeConEstado AS (
                SELECT 
                    FM.ASUNTO AS Asunto,
                    P.NOMBRE AS Proyecto,
                    CASE 
                        WHEN TP.TotalProductos = PM.ProductosMensaje THEN 'TODOS'
                        ELSE COALESCE(PR.DESCRIPCION, 'NO DESCRIPCION')
                    END AS Producto
                FROM MENSAJE M
                JOIN FORMATO_MENSAJE FM ON M.COD_FORMATO = FM.COD_FORMATO
                JOIN PROYECTO P ON M.PROYECTO = P.PROYECTO
                LEFT JOIN PRODUCTO PR ON M.PRODUCTO = PR.PRODUCTO
                LEFT JOIN TotalProductosPorProyecto TP ON P.PROYECTO = TP.PROYECTO
                LEFT JOIN ProductosMensaje PM ON M.PROYECTO = PM.PROYECTO AND M.COD_FORMATO = PM.COD_FORMATO
            )
            SELECT 
                Asunto,
                Proyecto,
                CASE 
                    WHEN COUNT(CASE WHEN Producto = 'TODOS' THEN 1 END) > 0 THEN 'TODOS'
                    ELSE MAX(Producto)
                END AS Producto
            FROM MensajeConEstado
            GROUP BY 
                Asunto,
                Proyecto; ";
            DataSet ds = dbHelper.ExecuteSelectCommand(query);
            gvResultados.DataSource = ds;
            gvResultados.DataBind();
        }

        protected void btnLeerXML_Click(object sender, EventArgs e)
        {
            string xmlPath = Server.MapPath("~/CreditCardInfo.xml");
            DataSet ds = new DataSet();
            ds.ReadXml(xmlPath);
            gvResultados.DataSource = ds;
            gvResultados.DataBind();
        }

    }
}