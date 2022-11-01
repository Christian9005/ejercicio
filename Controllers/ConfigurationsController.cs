using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Configuration.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationsController : ControllerBase
{
    private readonly IConfiguration configuration;

    
    public ConfigurationsController(IOptions<AplicacionConfiguracion> options ,IConfiguration configuration)
    {
        this.configuration = configuration;
        var valorConfigurado = options.Value.Informacion.Activo;
    }



    [HttpGet]
    public string ObtenerValor(string clave)
    {
        var valor = configuration.GetValue<string>(clave);

        if (string.IsNullOrEmpty(valor))
        {
            throw new ArgumentException($"La calve {clave}, no existe en la configuration ", clave);
            
        }

        return valor;
    }

    [HttpGet("obtener_activo")]
    public bool ObtenerValor()
    {
        var valor = configuration.GetValue<bool>("Aplicacion:Informacion:Activo");

        return valor;
    }

    [HttpGet("obtener_tipos_datos")]
    public string ObtenerTiposDatos()
    {
        var tipoNumeroEntero = configuration.GetValue<int>("TiposDatos:Numericos:Entero");
        var tipoNumeroDouble = configuration.GetValue<double>("TiposDatos:Numericos:Double");
        var tipoString = configuration.GetValue<string>("TiposDatos:Strings:Palabra");
        var tipoBool = configuration.GetValue<bool>("TiposDatos:Strings:Bool");

        return $"Tipo Entero: {tipoNumeroEntero}, Tipo Double: {tipoNumeroDouble}, TipoString: {tipoString}, Tipo Bool: {tipoBool}";        
    }

}

public class ConfigurationsLogging
{
    public LogLevel LogLevel {get; set;} 
}

public class LogLevel
{
    public string Default {get; set;}
    
    [JsonPropertyName("Microsoft.ApsNetCore")]
    public string MicrosoftApsNetcore {get; set;}
}
public class AplicacionConfiguracion
{
    public string NombreAplicacion {get; set;}
    public InformacionConfiguracion Informacion {get; set;}

}

public class InformacionConfiguracion 
{
    public int Version {get; set;}
    public string Pais {get; set;}
    public bool Activo {get; set;}
    
}
