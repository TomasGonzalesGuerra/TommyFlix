# 🏨 TommyFlix - Sistema de Reserva de Habitaciones Hotelera

[![.NET 10](https://shields.io)](https://microsoft.com)
[![Blazor](https://shields.io)](https://blazor.net)

Motor de reservas para el sector hotelero que integra la gestión de inventario de habitaciones, tarifas dinámicas basadas en temporadas, flujos de Check-In/Check-Out y un backend analítico para la administración del establecimiento.

## 🏛️ Enfoque Técnico y Desempeño

* **`TommyFlix.Api`**: Backend modular enfocado al alto rendimiento de consultas transaccionales.
* **`TommyFlix.Shared`**: Modelos conceptuales y DTOs reutilizables.
* **`TommyFlix.Web`**: Interfaz interactiva fluida para el usuario final y recepcionistas.

### Características Técnicas Clave
* **Control de Concurrencia:** Algoritmos en la API REST para evitar la sobre-reserva (*overbooking*) de habitaciones en transacciones simultáneas.
* **Consultas Avanzadas:** Optimización de lectura de disponibilidad mediante procedimientos almacenados y consultas complejas en **LINQ/T-SQL**.
* **UI Interactiva:** Panel de reservas construido en Blazor WebAssembly con controles de calendario elásticos.

## 🚀 Tecnologías Utilizadas

* **Backend:** C#, ASP.NET Core, EF Core, SQL Server / T-SQL de nivel avanzado.
* **Frontend:** Blazor WebAssembly, CSS Grid/Flexbox responsivo.

## 🛠️ Ejecución rápida
```bash
git clone https://github.com
cd TommyFlix
dotnet restore
# Ejecutar aplicación mediante solución global o comando .NET CLI
dotnet run --project TommyFlix.Api
```

---
Desarrollado por [Tomás Gonzales](https://linkedin.com).
