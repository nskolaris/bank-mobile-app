<?php
App::uses('AppController', 'Controller');

class ConfiguracionesController extends AppController{

	var $uses = array('Configuracion');

	function sincronizar(){
		if(isset($_POST['data'])){
			$post = json_decode($_POST['data']);
			if(!empty($post)){
				foreach($post as $configuracion){
					$configuracion = (array)$configuracion;
					if(!$this->Configuracion->getByNombreAndEvento($configuracion['denominacion'],$configuracion['evento_id'])){
						$this->Configuracion->guardar(array('Configuracion'=>array(
							'evento_id' => $configuracion['evento_id'],
							$configuracion['denominacion'] => $configuracion['valor']
						)));
					}
				}
			}
		}
		echo json_encode($this->Configuracion->getAll());
		exit;
	}
}
?>