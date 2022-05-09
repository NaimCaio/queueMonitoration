# Criar o container do rabbitmq
> docker-compose up

Isto irá criar a imagem e subir para um container
Apois criar o container adicione a fila files

# Voltar para pasta inicial do projeto e criar a imagem docker
> docker build -t consumer-rabbitmq-image -f Dockerfile .

# subir app
> docker run --name=filesRabbitMQContainer  -it  consumer-rabbitmq-image

# Rodar imagem do graphite para monitorar
> docker run -d  -p 80:80   -p 8125:8125/udp   graphiteapp/graphite-statsd
Depois só acessar localhost padrão para ver a monitoração rodando


# Exemplo aplicação funcionando local
![](images/metrics.PNG)
