using System;
using System.Collections.Generic;
using System.Linq;
using apiFestivos.Aplicacion;
using apiFestivos.Dominio;
using Moq;
using Xunit;
using apiFestivos.Dominio.Entidades;
using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;


public class FestivoServicioTests
{
    [Fact]
    public async Task EsFestivo_FechaFestivaDevuelveTrue()
    {
        var mockRepositorio = new Mock<IFestivoRepositorio>();
        mockRepositorio
            .Setup(repo => repo.ObtenerTodos())
            .ReturnsAsync(new List<Festivo>
            {
                new Festivo { Dia = 1, Mes = 1, IdTipo = 1, Nombre = "Año Nuevo" }
            });

        var servicio = new FestivoServicio(mockRepositorio.Object);
        var fechaFestiva = new DateTime(2024, 1, 1);

        var resultado = await servicio.EsFestivo(fechaFestiva);

        Assert.True(resultado);
    }

    [Fact]
    public async Task EsFestivo_FechaNoFestivaDevuelveFalse()
    {
        var mockRepositorio = new Mock<IFestivoRepositorio>();
        mockRepositorio
            .Setup(repo => repo.ObtenerTodos())
            .ReturnsAsync(new List<Festivo>
            {
                new Festivo { Dia = 1, Mes = 1, IdTipo = 1, Nombre = "Año Nuevo" }
            });

        var servicio = new FestivoServicio(mockRepositorio.Object);
        var fechaNoFestiva = new DateTime(2024, 1, 2);

        var resultado = await servicio.EsFestivo(fechaNoFestiva);

        Assert.False(resultado);
    }
  [Fact]
    public void ObtenerFestivo_Tipo1_RetornaFechaEsperada()
    {
        var mockRepositorio = new Mock<IFestivoRepositorio>();
        var festivoTipo1 = new Festivo { Dia = 1, Mes = 1, IdTipo = 1, Nombre = "Año Nuevo" };
        var servicio = new FestivoServicio(mockRepositorio.Object);
        var resultado = servicio.ObtenerFestivo(2024, festivoTipo1);

        Assert.Equal(new DateTime(2024, 1, 1), resultado.Fecha);
        Assert.Equal("Año Nuevo", resultado.Nombre);
    }
    [Fact]
      public void ObtenerFestivo_Tipo2_RetornaLunesSiguiente()
    {
        var mockRepositorio = new Mock<IFestivoRepositorio>();
        var festivoTipo2 = new Festivo { Dia = 1, Mes = 1, IdTipo = 2, Nombre = "Festivo Movible" };
        var servicio = new FestivoServicio(mockRepositorio.Object);

        var resultado = servicio.ObtenerFestivo(2024, festivoTipo2);

        var fechaEsperada = new DateTime(2024, 1, 8); 
        Assert.Equal(fechaEsperada, resultado.Fecha);
        Assert.Equal("Festivo Movible", resultado.Nombre);
    }
[Fact]
public void ObtenerFestivo_Tipo4_RetornaLunesSiguienteSemanaSanta()
{
    var mockRepositorio = new Mock<IFestivoRepositorio>();
    var festivoTipo4 = new Festivo 
    { 
        Dia = 1, 
        Mes = 4, 
        IdTipo = 4,
        Nombre = "Festivo Semana Santa"
    };

    var servicio = new FestivoServicio(mockRepositorio.Object);
    var resultado = servicio.ObtenerFestivo(2024, festivoTipo4);
    var fechaEsperada = new DateTime(2024, 4, 1);
    Assert.Equal(fechaEsperada, resultado.Fecha);
    Assert.Equal("Festivo Semana Santa", resultado.Nombre);
}


}
