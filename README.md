
# 📡 Proyecto de Arquitectura de Software: Publicador - Subscriptor con Azure Service Bus

Este proyecto implementa el patrón de diseño **Publicador-Subscriptor (Publisher-Subscriber)** usando **Azure Service Bus** y tecnología **.NET 6/8**.  
La solución contiene dos proyectos que simulan el flujo de pagos de nómina de forma desacoplada mediante mensajería basada en eventos.

---

## 🧩 Estructura del Proyecto

```
PayrollSystemSolution/
├── Publicador/           # API Web (ASP.NET Core) que publica eventos en un tópico
│   ├── Controllers/      # Endpoints para publicar solicitudes de pago
│   ├── Services/         # Lógica para conectar con Azure Service Bus
│   ├── DTOs/             # Modelo de la solicitud
│   └── Config/           # Configuración cargada desde appsettings o User Secrets
│
├── Subscriptor/          # Aplicación de consola (Console App) que escucha y procesa mensajes
│   ├── Services/         # Simulación del procesamiento del pago
│   ├── DTOs/             # Modelo de la solicitud
│   └── Config/           # Configuración vía User Secrets
```

---

## 🧠 Patrón Aplicado: Publicador - Subscriptor

Este patrón permite la comunicación asincrónica y desacoplada entre sistemas.

- **Publicador:** Publica mensajes en un `Topic` de Azure Service Bus.
- **Subscriptor:** Escucha mensajes desde una `Subscription` y ejecuta acciones.

---

## 🚀 Tecnologías Utilizadas

- .NET 8
- ASP.NET Core Web API
- Azure Service Bus (Topics & Subscriptions)
- User Secrets para manejo seguro de secretos en desarrollo
- Visual Studio 2022

---

## 🔧 Configuración Requerida

### 1. Azure Service Bus

- Crea un **Namespace** de tipo **Standard** en Azure.
- Crea un **Topic** llamado `pagonomina`.
- Crea una **Subscription** llamada `subscriptorNomina`.
- Copia la **Connection String** de la política `RootManageSharedAccessKey`.

---

### 2. Configurar Publicador

#### Opción recomendada: usar `User Secrets`

```bash
cd Publicador
dotnet user-secrets init
dotnet user-secrets set "ServiceBus:ConnectionString" "<TU_CADENA_DE_CONEXION>"
```

Y en `appsettings.json`:

```json
"ServiceBus": {
  "ConnectionString": "",
  "TopicName": "pagonomina"
}
```

---

### 3. Configurar Subscriptor

#### También usando `User Secrets`:

```bash
cd Subscriptor
dotnet user-secrets init
dotnet user-secrets set "ServiceBus:ConnectionString" "<TU_CADENA_DE_CONEXION>"
dotnet user-secrets set "ServiceBus:TopicName" "pagonomina"
dotnet user-secrets set "ServiceBus:SubscriptionName" "subscriptorNomina"
```

---

## 📬 Cómo Probar

1. Ejecuta el proyecto **Publicador**.
![image](https://github.com/user-attachments/assets/97037849-6b15-4a79-812b-9371aace171b)

2. Envia una solicitud POST usando Postman o Swagger 
![image](https://github.com/user-attachments/assets/3a40612d-20eb-4fad-a938-96226177e691)

![image](https://github.com/user-attachments/assets/baba40fc-56ae-4a7e-a3cd-66f1230a9ead)

```
POST http://localhost:{puerto}/api/PagoDeNomina/send
```

Body (JSON):

```json
{
  "employeeName": "Ana García",
  "amount": 1800000
}
```

3. Ejecuta el proyecto **Subscriptor**.
   
En el visual studio 2022 dar click derecho sobre el proyecto "Subscriptor" y seleccionar la opción
Debug - Start New Instance

![image](https://github.com/user-attachments/assets/beee3ea0-dc3f-4cf2-9a16-b47099ef1574)

5. Verás en consola que el mensaje fue recibido y procesado correctamente.
![image](https://github.com/user-attachments/assets/2b1c384d-4c15-4014-8d81-e3ae9922d7d2)

---

---

## 👨‍🎓 Créditos

Proyecto desarrollado como parte de la materia **Arquitectura de Software**.  
Implementa un patrón clásico aplicado a una solución moderna basada en servicios.

---
