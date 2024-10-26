﻿using Entity;
using Mapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PeliculaDao
    {
        private CategoriaDao CategoriaDao = new CategoriaDao();

        public void cargarPelicula(Pelicula pelicula)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(ConexionesDB.GetConexionUAIStreaming()))
                {
                    con.Open();
                    string query = "INSERT INTO PELICULA (ID_CATEGORIA, TITULO, DESCRIPCION, ANIO_LANZAMIENTO, DURACION, CALIFICACION, FECHA_ALTA) VALUES (@idCategoria, @titulo, @descripcion, @anioLanzamiento, @duracion, @calificacion, @fechaAlta)";
                    using(SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@idCategoria", pelicula.Categoria.IdCategoria);
                        command.Parameters.AddWithValue("@titulo", pelicula.Titulo);
                        command.Parameters.AddWithValue("@descripcion", pelicula.Descripcion);
                        command.Parameters.AddWithValue("@anioLanzamiento", pelicula.AñoLanzamiento);
                        command.Parameters.AddWithValue("@duracion", pelicula.Duracion);
                        command.Parameters.AddWithValue("@calificacion", pelicula.Calificacion);
                        command.Parameters.AddWithValue("@fechaAlta", pelicula.FechaAlta);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void eliminarPelicula(int idPelicula)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionesDB.GetConexionUAIStreaming()))
                {
                    con.Open();
                    string query = "DELETE FROM PELICULA WHERE ID_PELICULA = @idPelicula";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@idPelicula", idPelicula);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void modificarCalificacion(int idPelicula, int nuevaCalificacion)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionesDB.GetConexionUAIStreaming()))
                {
                    con.Open();
                    string query = "UPDATE PELICULA SET CALIFICACION = @calificacion WHERE ID_PELICULA = @idPelicula";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@idPelicula", idPelicula);
                        command.Parameters.AddWithValue("@calificacion", nuevaCalificacion);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Pelicula> GetPeliculas()
        {
            try
            {
                List<Pelicula> listaPeliculas = new List<Pelicula>();
                using(SqlConnection con = new SqlConnection(ConexionesDB.GetConexionUAIStreaming()))
                {
                    con.Open();
                    string query = "SELECT * FROM PELICULA";
                    using(SqlCommand command = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idCategoria = Convert.ToInt32(reader["ID_CATEGORIA"].ToString());
                                Categoria categoria = CategoriaDao.GetById(idCategoria);
                                listaPeliculas.Add(PeliculaMapper.Map(reader, categoria));
                            }
                        }
                    }
                }
                return listaPeliculas;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
