# mhealth-diabetes
Proyecto de desarrollo de software


#Estructura:
Nos estamos basando en la arquitectura de propuesta que está en drive.
Esta arquitectura nos propone microservicios que en el sentido práctico son proyectos/soluciones separadas. Para ello se creó la carpeta de microservices y estamos nombrando los componentes de acuerdo a los componentes de arquitectura propuestos.

El primer microservicio creado está descrito a continuación:
/mhealth-diabetes/microservices/services/cacfileupload

##Se creó la estructura así:

	El primer modulo a implementar será CACFileUpload, como corresponde en el diagrama de arquitectura.
	Este proyecto está estructurado así:
		2 proyectos, el primer proyecto es CAC.Library que será el encargado de administrar el modelo (entidades de datos y entidades de transporte), utilidades, y clases en general.

	El segundo proyecto es CAC.Services que será un proyecto en WCF. Este expondrá los servicios en la Web mediante comportamiento API. Tiene la siguiente estructura Interfaces (son las interfaces publicas para ser consultadas) y Services: las implementaciones de las interfaces.
