namespace MvcNetCoreLinqToSqlInjection.Models
{
    public class Deportivo : ICoche
    {
        public Deportivo() {

            this.Marca = "Triciclo";
            this.Modelo = "Guay";
            this.Imagen = "deportivo.png";
            this.Velocidad = 0;
            this.VelocidadMaxima = 320;

        }

        public string Marca {  get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }

        public void Acelerar()
        {
            this.Velocidad += 45;
            if(this.Velocidad >= this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
        }

        public void Frenar()
        {
            this.Velocidad = -15;
            if(this.Velocidad < 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
