<?php
App::uses('AppController', 'Controller');

class ConfiguracionesController extends AppController{

	var $uses = array('Configuracion','Evento');
	
	function gestionarPorEvento($evento_id){
		if(!empty($this->data)){
			$result = $this->Configuracion->guardar($this->data);
		}
		$this->set('evento',$this->Evento->getById($evento_id));
		$this->set('configuraciones',$this->Configuracion->getByEvento($evento_id));
	}
	
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