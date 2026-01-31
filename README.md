# Sistema de Gestión Médica – ASP.NET Core MVC

Sistema web desarrollado en ASP.NET Core MVC 
para la gestión de Pacientes, Doctores y Citas Médicas, 
con autenticación de usuarios mediante Identity 
y una interfaz usando Bootstrap.



##  Tecnologías Utilizadas

- ✅ ASP.NET Core MVC (.NET 8/9/10)
- ✅ Entity Framework Core
- ✅ SQL Server
- ✅ ASP.NET Identity (Login y Registro de usuarios)
- ✅ Bootstrap 5 (Diseño responsive)
- ✅ JavaScript Fetch API (AJAX)
- ✅ Modales dinámicos para formularios

---

## Módulos del Sistema

### Pacientes
- Crear pacientes
- Editar información
- Listar pacientes
- Eliminar pacientes

### Doctores
- Registro de doctores
- Especialidades médicas
- Gestión de datos personales

### Citas Médicas
- Programar citas
- Selección de paciente y doctor desde combos dinámicos
- Edición desde ventanas modales
- Eliminación de citas
- Listado con tabla estilizada Bootstrap

---

## Autenticación

El sistema usa ASP.NET Identity para:

- Registro de usuarios
- Inicio de sesión
- Cierre de sesión
- Protección de rutas por usuario autenticado

---

## Interfaz de Usuario

El sistema cuenta con:

 Menú de navegación superior  
 Tablas responsivas con Bootstrap  
 Formularios en ventanas modales  
 Select dinámicos para Pacientes y Doctores  
 Diseño adaptable a móviles  

---

## Instalación del Proyecto

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SistemaCitasDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

## Terminal
Add-Migration InitialCreate
Update-Database

dotnet run


### Clonar el repositorio
 bash
git clone https://github.com/oscarofpcc-coder/SisGestHospitalEP1.git
