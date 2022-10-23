<h1>Шаблон для запуска микросервисов</h1>

<h3>Общее описание сервисов</h3>
<p>Сервисы разработаны в соответствии с чистой архитектурой.<br/>
В каждом микросервисе используется 5 проектов:
<ul> 
<li>Domain - доменные сущности </li>
<li>Interfaces - абстракции в т.ч. для Data Access Layer </li>
<li>Logic - бизнес логика</li>
<li>Infrastructure - инфраструктура проекта (провайдеры доступа к данным, шине, пр...)</li>
<li>WebApi - внешний слой для взаимодействия по REST API</li>
</ul>
</p>

<p> В solution используется
<ul> 
<li>ProblemDetails для обработки ошибок в микросервисе и передачи на фронт</li>
<li>Automapper </li>
<li>Mediatr</li>
<li>FluentValidation</li>
<li>EF Core Npgsql провайдер для работы с базой</li>
<li>Serilog</li>
<li>Refit</li>
<li>X.PagedList</li>
</ul>

</p>
<p> Для миграций используем команжу dotnet ef migrations add migrationName -s ..\Messages.WebApi\Messages.WebApi.csproj</p>

