#!/bin/bash

set -e  # 🚨 Detener en caso de error

MODELS_DIR="Models"
CONTEXT_NAME="AppDbContext"  # Asegúrate de usar el namespace completo si es necesario
CONTROLLERS_DIR="Controllers"

# Crear carpeta de controladores si no existe
mkdir -p "$CONTROLLERS_DIR"

# Generar controladores para cada modelo
for MODEL_FILE in "$MODELS_DIR"/*.cs; do
    MODEL_NAME=$(basename "$MODEL_FILE" .cs)
    CONTROLLER_NAME="${MODEL_NAME}Controller"

    echo "📦 Generando controlador API para $MODEL_NAME..."

    # Comando para generar el controlador de la API
    dotnet aspnet-codegenerator controller \
        -f \
        -api \
        -name "$CONTROLLER_NAME" \
        -m "$MODEL_NAME" \
        -dc "$CONTEXT_NAME" \
        --relativeFolderPath "$CONTROLLERS_DIR" \
        --useAsyncActions \
        --noViews  # No generar vistas
done

echo "✅ ¡Controladores API generados!"
