function mostrarAlerta(mensaje) {
    alert(mensaje);
}

function obtenerHoraActual() {
    return new Date().toLocaleTimeString();
}


function llamarMetodoCSharp() {
    let NombreDelAssembly = 'EjemploWeb'; // nombre del proyecto
    DotNet.invokeMethodAsync(NombreDelAssembly, 'MostrarMensajeDesdeJS', 'Hola desde JS')
        .then(_ => console.log("Método C# invocado desde JS"));
}


function llamarInstanciaCSharp(dotnetHelper) {
    dotnetHelper.invokeMethodAsync('MostrarInstancia', 'Mensaje desde JS instancia');
}