const toBold = (content: string): string => `<strong>${content}</strong>`;

export const TEXTS = {
  FaceTec_accessibility_cancel_button: 'Cancelar',

  FaceTec_feedback_center_face: 'Centralize seu Rosto',
  FaceTec_feedback_face_not_found: 'Enquadre seu Rosto',
  FaceTec_feedback_move_phone_away: 'Afaste-se',
  FaceTec_feedback_move_away_web: 'Afaste-se',
  FaceTec_feedback_move_phone_closer: 'Aproxime-se',
  FaceTec_feedback_move_phone_to_eye_level: 'Telefone ao nível dos olhos',
  FaceTec_feedback_move_to_eye_level_web: 'Olhe para a câmera',
  FaceTec_feedback_face_not_looking_straight_ahead: 'Olhe para frente',
  FaceTec_feedback_face_not_upright: 'Mantenha a cabeça reta',
  FaceTec_feedback_face_not_upright_mobile: 'Mantenha a cabeça reta',
  FaceTec_feedback_hold_steady: 'Segure firme',
  FaceTec_feedback_move_web_closer: 'Aproxime-se',
  FaceTec_feedback_move_web_even_closer: 'Mais próximo',
  FaceTec_feedback_use_even_lighting: 'Ilumine seu rosto uniformemente',

  FaceTec_instructions_header_ready_desktop: toBold(
    'Prepare-se para gravar o seu Vídeo Selfie!',
  ),
  FaceTec_instructions_header_ready_1_mobile: toBold('Prepare-se para gravar'),
  FaceTec_instructions_header_ready_2_mobile: toBold('o seu Vídeo Selfie!'),
  FaceTec_instructions_message_ready_desktop:
    'Por favor, enquadre seu rosto dentro da menor forma oval e depois na maior forma oval.',
  FaceTec_instructions_message_ready_1_mobile:
    'Por favor, enquadre seu rosto dentro da menor forma oval',
  FaceTec_instructions_message_ready_2_mobile: 'e depois na maior forma oval.',
  FaceTec_action_im_ready: 'Gravar meu Vídeo Selfie',

  FaceTec_presession_frame_your_face: 'Centralize seu Rosto',
  FaceTec_presession_look_straight_ahead: 'Olhe para frente',
  FaceTec_presession_hold_steady3: 'Segure firme',
  FaceTec_presession_hold_steady2: 'Segure firme: 1',
  FaceTec_presession_hold_steady1: 'Segure firme: 2',
  FaceTec_presession_eyes_straight_ahead: 'Olhe para frente',
  FaceTec_presession_remove_dark_glasses: 'Tire o óculos escuro',
  FaceTec_presession_neutral_expression:
    'Faça uma expressão neutra, sem sorrir',
  FaceTec_presession_conditions_too_bright:
    'Evite excesso de brilho ou luz extrema',
  FaceTec_presession_brighten_your_environment: 'Ambiente muito escuro',

  FaceTec_result_facescan_upload_message: toBold(
    'Estamos fazendo o upload<br/>do seu Vídeo Selfie',
  ),
  FaceTec_result_idscan_upload_message: toBold(
    'Estamos fazendo o upload<br/>do seu Documento',
  ),

  FaceTec_retry_header: toBold('Vamos tentar seu Vídeo Selfie novamente!'),
  FaceTec_retry_subheader_message:
    'Antes disso, certifique-se de que você não possui acessórios no rosto e opte por um ambiente iluminado.',
  FaceTec_retry_your_image_label: toBold('Sua selfie'),
  FaceTec_retry_ideal_image_label: toBold('Pose ideal'),
  FaceTec_retry_instruction_message_1: toBold(
    'Faça uma expressão neutra, sem sorrir',
  ),
  FaceTec_retry_instruction_message_2: toBold(
    'Evite excesso de brilho ou luz extrema',
  ),
  FaceTec_retry_instruction_message_3: toBold(
    'Muito borrado, limpe sua câmera e segure firme',
  ),
  FaceTec_action_try_again: 'Gravar meu Vídeo Selfie',

  FaceTec_camera_feed_issue_header: '<b>Issue Encrypting Camera Feed</b>',
  FaceTec_camera_feed_issue_subheader_message:
    'This system cannot be verified due to the following:',
  FaceTec_camera_feed_issue_table_header_1: 'Possible Issue',
  FaceTec_camera_feed_issue_table_header_2: 'Fix',
  FaceTec_camera_feed_issue_table_row_1_cell_1_firefox_permissions_error:
    'Camera permissions not remembered in Firefox.',
  FaceTec_camera_feed_issue_table_row_1_cell_2_firefox_permissions_error:
    'Check Remember Permissions.',
  FaceTec_camera_feed_issue_table_row_1_cell_1:
    'Camera already in use by another App.',
  FaceTec_camera_feed_issue_table_row_1_cell_2: 'Close the other App.',
  FaceTec_camera_feed_issue_table_row_2_cell_1:
    'A 3rd-Party App is modifying the video.',
  FaceTec_camera_feed_issue_table_row_2_cell_2:
    'Close/Uninstall the other App.',
  FaceTec_camera_feed_issue_table_row_3_cell_1:
    'Hardware not capable of being secured.',
  FaceTec_camera_feed_issue_table_row_3_cell_2: 'Use a different Device.',
  FaceTec_camera_feed_issue_subtable_message:
    "This App blocks suspicious webcam configurations. <a href='https://livenesscheckhelp.com/' target='_blank' style='text-decoration:underline;'>Learn More Here</a>.",
  FaceTec_camera_feed_issue_action: 'Try Again Anyway',
  FaceTec_camera_feed_issue_action_firefox_permissions_error: 'OK',

  FaceTec_camera_permission_header: 'Ativar a câmera',
  FaceTec_camera_permission_message:
    'O acesso à câmera está desativado. Toque abaixo para ativar em ajustes.',
  FaceTec_action_ok: 'Ativar',

  FaceTec_enter_fullscreen_header: 'Full Screen Selfie Mode',
  FaceTec_enter_fullscreen_message:
    'Before we begin, please click the button below to open full screen mode',
  FaceTec_enter_fullscreen_action: 'Open Full Screen',

  FaceTec_initializing_camera: 'Iniciando a câmera...',
  FaceTec_initializing_camera_still_loading: 'Iniciando a câmera...',

  FaceTec_idscan_type_selection_header: 'Tipo do Documento',
  FaceTec_action_select_id_card: 'Documento Com Foto',
  FaceTec_action_select_passport: 'PASSAPORTE',
  FaceTec_idscan_capture_id_card_front_instruction_message:
    'Mostre a Frente do Documento Com Foto',
  FaceTec_idscan_capture_id_card_back_instruction_message:
    'Mostre o Verso do Documento',
  FaceTec_idscan_capture_passport_instruction_message:
    'Mostre a Página da Foto no Passaporte',
  FaceTec_action_take_photo: 'TIRAR FOTO',
  FaceTec_idscan_review_id_card_front_instruction_message:
    'Confirme Se a Foto Está Clara e Legível',
  FaceTec_idscan_review_id_card_back_instruction_message:
    'Confirme Se Está Claro e Legível',
  FaceTec_idscan_review_passport_instruction_message:
    'Confirme Se a Foto Está Clara e Legível',
  FaceTec_action_accept_photo: 'ACEITAR',
  FaceTec_action_retake_photo: 'RECAPTURAR',
  FaceTec_result_idscan_unsuccess_message:
    'Foto do Documento Nāo Corresponde com Rosto do Usuário',

  FaceTec_result_success_message: 'Sucesso!',
};
