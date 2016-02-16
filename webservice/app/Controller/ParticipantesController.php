<?php
App::uses('AppController', 'Controller');

class ParticipantesController extends AppController{

	var $uses = array('Participante');

	function sincronizar(){
		$post = json_decode($_POST['data']);
		if(!empty($post)){
			$array_guardados = array();
			foreach($post as $participante){
				$participante = (array)$participante;
				if(!$existe = $this->Participante->getByCodeEmail($participante['evento_code'],$participante['email'])){
					$response = $this->Participante->agregar(array('Participante'=>$participante));
					if($response['status']){
						$array_guardados[$participante['local_id']] = $this->Participante->id;
					}
				}else{
					$array_guardados[$participante['local_id']] = $existe['Participante']['id'];
				}
			}
			echo json_encode($array_guardados);
		}
		exit;
	}
}
?>