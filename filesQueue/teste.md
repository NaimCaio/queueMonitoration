# Acessar pasta rabbitmq-go e rodar
> docker-compose up

Isto irá criar a imagem e subir para um container

# Voltar para pasta inicial do projeto e criar a imagem docker
> docker build -t consumer-rabbitmq-image -f Dockerfile .

# Rodar imagem do graphite para monitorar
> docker run -d  -p 80:80   -p 8125:8125/udp   graphiteapp/graphite-statsd
Depois só acessar localhost padrão para ver a monitoração rodando

# subir app
> docker run --name=filesRabbitMQContainer  -it  consumer-rabbitmq-image

# Exemplo aplicação funcionando local
![](images/metrics.PNG)
